BeforeAll {
    $script:Script = Join-Path $PSScriptRoot 'Assert-VersionIsNewer.ps1'
}

Describe 'Assert-VersionIsNewer' {
    Context 'As a workflow run checking a change, when the latest release is 2.3.4' {
        BeforeAll {
            $script:Released = @('2.3.2', '2.3.3', '2.3.4')
        }

        It 'Should pass a <Bump> bump to <Version>' -TestCases @(
            @{ Bump = 'patch'; Version = '2.3.5' }
            @{ Bump = 'patch'; Version = '2.3.6' }
            @{ Bump = 'patch'; Version = '2.3.10' }
            @{ Bump = 'patch'; Version = '2.3.90210' }
            @{ Bump = 'minor'; Version = '2.4.0' }
            @{ Bump = 'minor'; Version = '2.4.1' }
            @{ Bump = 'minor'; Version = '2.4.4' }
            @{ Bump = 'minor'; Version = '2.4.5' }
            @{ Bump = 'minor'; Version = '2.4.10' }
            @{ Bump = 'minor'; Version = '2.5.0' }
            @{ Bump = 'minor'; Version = '2.5.1' }
            @{ Bump = 'minor'; Version = '2.5.4' }
            @{ Bump = 'minor'; Version = '2.5.5' }
            @{ Bump = 'minor'; Version = '2.5.10' }
            @{ Bump = 'minor'; Version = '2.10.0' }
            @{ Bump = 'minor'; Version = '2.10.1' }
            @{ Bump = 'minor'; Version = '2.10.4' }
            @{ Bump = 'minor'; Version = '2.10.5' }
            @{ Bump = 'minor'; Version = '2.10.10' }
            @{ Bump = 'minor'; Version = '2.42.0' }
            @{ Bump = 'minor'; Version = '2.42.1' }
            @{ Bump = 'minor'; Version = '2.42.4' }
            @{ Bump = 'minor'; Version = '2.42.5' }
            @{ Bump = 'minor'; Version = '2.42.10' }
            @{ Bump = 'minor'; Version = '2.42.90210' }
            @{ Bump = 'major'; Version = '3.0.0' }
            @{ Bump = 'major'; Version = '3.0.1' }
            @{ Bump = 'major'; Version = '3.0.4' }
            @{ Bump = 'major'; Version = '3.0.5' }
            @{ Bump = 'major'; Version = '3.0.10' }
            @{ Bump = 'major'; Version = '3.1.0' }
            @{ Bump = 'major'; Version = '3.1.1' }
            @{ Bump = 'major'; Version = '3.1.4' }
            @{ Bump = 'major'; Version = '3.1.5' }
            @{ Bump = 'major'; Version = '3.1.10' }
            @{ Bump = 'major'; Version = '3.3.0' }
            @{ Bump = 'major'; Version = '3.3.1' }
            @{ Bump = 'major'; Version = '3.3.4' }
            @{ Bump = 'major'; Version = '3.3.5' }
            @{ Bump = 'major'; Version = '3.3.10' }
            @{ Bump = 'major'; Version = '3.4.0' }
            @{ Bump = 'major'; Version = '3.4.1' }
            @{ Bump = 'major'; Version = '3.4.4' }
            @{ Bump = 'major'; Version = '3.4.5' }
            @{ Bump = 'major'; Version = '3.4.10' }
            @{ Bump = 'major'; Version = '3.10.0' }
            @{ Bump = 'major'; Version = '3.10.1' }
            @{ Bump = 'major'; Version = '3.10.4' }
            @{ Bump = 'major'; Version = '3.10.5' }
            @{ Bump = 'major'; Version = '3.10.10' }
            @{ Bump = 'major'; Version = '4.0.0' }
            @{ Bump = 'major'; Version = '4.0.1' }
            @{ Bump = 'major'; Version = '4.0.4' }
            @{ Bump = 'major'; Version = '4.0.5' }
            @{ Bump = 'major'; Version = '4.0.10' }
            @{ Bump = 'major'; Version = '4.1.0' }
            @{ Bump = 'major'; Version = '4.1.1' }
            @{ Bump = 'major'; Version = '4.1.4' }
            @{ Bump = 'major'; Version = '4.1.5' }
            @{ Bump = 'major'; Version = '4.1.10' }
            @{ Bump = 'major'; Version = '4.3.0' }
            @{ Bump = 'major'; Version = '4.3.1' }
            @{ Bump = 'major'; Version = '4.3.4' }
            @{ Bump = 'major'; Version = '4.3.5' }
            @{ Bump = 'major'; Version = '4.3.10' }
            @{ Bump = 'major'; Version = '4.4.0' }
            @{ Bump = 'major'; Version = '4.4.1' }
            @{ Bump = 'major'; Version = '4.4.4' }
            @{ Bump = 'major'; Version = '4.4.5' }
            @{ Bump = 'major'; Version = '4.4.10' }
            @{ Bump = 'major'; Version = '4.10.0' }
            @{ Bump = 'major'; Version = '4.10.1' }
            @{ Bump = 'major'; Version = '4.10.4' }
            @{ Bump = 'major'; Version = '4.10.5' }
            @{ Bump = 'major'; Version = '4.10.10' }
            @{ Bump = 'major'; Version = '10.0.0' }
            @{ Bump = 'major'; Version = '10.0.1' }
            @{ Bump = 'major'; Version = '10.0.4' }
            @{ Bump = 'major'; Version = '10.0.5' }
            @{ Bump = 'major'; Version = '10.0.10' }
            @{ Bump = 'major'; Version = '10.1.0' }
            @{ Bump = 'major'; Version = '10.1.1' }
            @{ Bump = 'major'; Version = '10.1.4' }
            @{ Bump = 'major'; Version = '10.1.5' }
            @{ Bump = 'major'; Version = '10.1.10' }
            @{ Bump = 'major'; Version = '10.3.0' }
            @{ Bump = 'major'; Version = '10.3.1' }
            @{ Bump = 'major'; Version = '10.3.4' }
            @{ Bump = 'major'; Version = '10.3.5' }
            @{ Bump = 'major'; Version = '10.3.10' }
            @{ Bump = 'major'; Version = '10.4.0' }
            @{ Bump = 'major'; Version = '10.4.1' }
            @{ Bump = 'major'; Version = '10.4.4' }
            @{ Bump = 'major'; Version = '10.4.5' }
            @{ Bump = 'major'; Version = '10.4.10' }
            @{ Bump = 'major'; Version = '10.10.0' }
            @{ Bump = 'major'; Version = '10.10.1' }
            @{ Bump = 'major'; Version = '10.10.4' }
            @{ Bump = 'major'; Version = '10.10.5' }
            @{ Bump = 'major'; Version = '10.10.10' }
            @{ Bump = 'major'; Version = '600.42.10' }
            @{ Bump = 'major'; Version = '600.42.90210' }
        ) {
            { & $Script -Version $Version -ReleasedVersions $Released } | Should -Not -Throw
        }

        It 'Should fail a version that was not bumped, naming both versions' {
            $failure = { & $Script -Version '2.3.4' -ReleasedVersions $Released } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Version 2.3.4 must be greater than the highest released version, 2.3.4.'
        }

        It 'Should fail a <Bump> drop to <Version>, naming both versions' -TestCases @(
            @{ Bump = 'major'; Version = '0.0.0' }
            @{ Bump = 'major'; Version = '0.9.10' }
            @{ Bump = 'major'; Version = '1.0.0' }
            @{ Bump = 'major'; Version = '1.7.8' }
            @{ Bump = 'minor'; Version = '2.1.0' }
            @{ Bump = 'minor'; Version = '2.1.6' }
            @{ Bump = 'minor'; Version = '2.2.0' }
            @{ Bump = 'minor'; Version = '2.2.5' }
            @{ Bump = 'patch'; Version = '2.3.2' }
            @{ Bump = 'patch'; Version = '2.3.3' }
        ) {
            $failure = { & $Script -Version $Version -ReleasedVersions $Released } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be "Version $Version must be greater than the highest released version, 2.3.4."
        }
    }

    Context 'As a workflow run checking a change, when a release is tagged with a leading v' {
        It 'Should count v3.0.0 as 3.0.0 for failures' {
            $failure = { & $Script -Version '3.0.0' -ReleasedVersions @('2.0.1', 'v3.0.0') } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Version 3.0.0 must be greater than the highest released version, 3.0.0.'
        }
        
        It 'Should count v3.0.0 as 3.0.0 for successes' {
            { & $Script -Version '3.0.1' -ReleasedVersions @('2.0.1', 'v3.0.0') } | Should -Not -Throw
        }
    }

    Context 'As a workflow run checking a change, when releases are listed out of order' {
        It 'Should compare against the highest release, not the first or last listed' {
            $failure = { & $Script -Version '5.0.5' -ReleasedVersions @('4.1.5', '5.0.5', '1.0.0', 'v3.0.0', '5.0.0') } | Should -Throw -PassThru

            $failure.Exception.Message | Should -Be 'Version 5.0.5 must be greater than the highest released version, 5.0.5.'
        }
    }

    Context 'As a workflow run checking the first change of a new repository, with no releases' {
        It 'Should pass any version' {
            { & $Script -Version '1.0.0' -ReleasedVersions @() } | Should -Not -Throw
        }
    }
}
