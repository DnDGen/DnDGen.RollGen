BeforeAll {
    $script:Script = Join-Path $PSScriptRoot 'Assert-CoverageNotLower.ps1'

    function New-CoverageReport {
        param(
            [string]$Name,
            [int]$LinesCovered,
            [int]$LinesValid,
            [int]$BranchesCovered,
            [int]$BranchesValid
        )

        $path = Join-Path $TestDrive "$Name.cobertura.xml"
        $xml = '<?xml version="1.0" encoding="utf-8"?>' +
            "<coverage line-rate=`"1`" branch-rate=`"1`" lines-covered=`"$LinesCovered`" lines-valid=`"$LinesValid`" branches-covered=`"$BranchesCovered`" branches-valid=`"$BranchesValid`"><packages /></coverage>"
        Set-Content -Path $path -Value $xml -Encoding UTF8

        return $path
    }
}

Describe 'Assert-CoverageNotLower' {
    Context 'As a workflow run checking a change, when the latest release covers 215/215 lines and 58/58 branches' {
        BeforeAll {
            $script:Baseline = New-CoverageReport -Name 'baseline' -LinesCovered 215 -LinesValid 215 -BranchesCovered 58 -BranchesValid 58
        }

        It 'Should pass equal coverage' {
            $report = New-CoverageReport -Name 'equal' -LinesCovered 215 -LinesValid 215 -BranchesCovered 58 -BranchesValid 58

            { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Not -Throw
        }

        It 'Should pass one more covered line' {
            $report = New-CoverageReport -Name 'more-lines' -LinesCovered 216 -LinesValid 216 -BranchesCovered 58 -BranchesValid 58

            { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Not -Throw
        }

        It 'Should fail one fewer covered line, stating both line figures' {
            $report = New-CoverageReport -Name 'fewer-lines' -LinesCovered 214 -LinesValid 215 -BranchesCovered 58 -BranchesValid 58

            $failure = { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Line coverage dropped from 215/215 (100.00%) to 214/215 (99.53%).'
        }

        It 'Should fail one fewer covered branch, stating both branch figures' {
            $report = New-CoverageReport -Name 'fewer-branches' -LinesCovered 215 -LinesValid 215 -BranchesCovered 57 -BranchesValid 58

            $failure = { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Branch coverage dropped from 58/58 (100.00%) to 57/58 (98.28%).'
        }

        It 'Should fail when both drop, stating both drops' {
            $report = New-CoverageReport -Name 'both-fewer' -LinesCovered 214 -LinesValid 215 -BranchesCovered 57 -BranchesValid 58

            $failure = { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Line coverage dropped from 215/215 (100.00%) to 214/215 (99.53%). Branch coverage dropped from 58/58 (100.00%) to 57/58 (98.28%).'
        }

        It 'Should pass more lines at the same fraction' {
            $report = New-CoverageReport -Name 'same-fraction' -LinesCovered 430 -LinesValid 430 -BranchesCovered 58 -BranchesValid 58

            { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Not -Throw
        }

        It 'Should fail more covered lines at a lower fraction' {
            $report = New-CoverageReport -Name 'lower-fraction' -LinesCovered 216 -LinesValid 217 -BranchesCovered 58 -BranchesValid 58

            $failure = { & $Script -ReportPath $report -BaselinePath $Baseline } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Line coverage dropped from 215/215 (100.00%) to 216/217 (99.54%).'
        }
    }

    Context 'As a workflow run checking a change, when the latest release has no coverage report' {
        It 'Should fail, naming the missing report' {
            $report = New-CoverageReport -Name 'change' -LinesCovered 215 -LinesValid 215 -BranchesCovered 58 -BranchesValid 58
            $missing = Join-Path $TestDrive 'missing.cobertura.xml'

            $failure = { & $Script -ReportPath $report -BaselinePath $missing } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be "No coverage report for the latest release: $missing."
        }
    }

    Context 'As a workflow run checking a change, when there are no branches to cover' {
        It 'Should pass zero valid branches on both sides' {
            $baseline = New-CoverageReport -Name 'no-branches-baseline' -LinesCovered 10 -LinesValid 10 -BranchesCovered 0 -BranchesValid 0
            $report = New-CoverageReport -Name 'no-branches-change' -LinesCovered 10 -LinesValid 10 -BranchesCovered 0 -BranchesValid 0

            { & $Script -ReportPath $report -BaselinePath $baseline } | Should -Not -Throw
        }

        It 'Should fail a change that adds an uncovered branch where the release had none' {
            $baseline = New-CoverageReport -Name 'none-baseline' -LinesCovered 10 -LinesValid 10 -BranchesCovered 0 -BranchesValid 0
            $report = New-CoverageReport -Name 'first-branches' -LinesCovered 10 -LinesValid 10 -BranchesCovered 1 -BranchesValid 2

            $failure = { & $Script -ReportPath $report -BaselinePath $baseline } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Branch coverage dropped from 0/0 (100.00%) to 1/2 (50.00%).'
        }
    }
}
