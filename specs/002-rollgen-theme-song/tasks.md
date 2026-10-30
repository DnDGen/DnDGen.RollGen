---

description: "Task list for the DnDGen.RollGen theme song"
---

# Tasks: DnDGen.RollGen Theme Song

**Input**: Design documents from `specs/002-rollgen-theme-song/`

**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md)

**Tests**: Written first, from DnDGen.Stress's approved list, and seen to fail before the README
changes that make them pass (II).

## Format: `[ID] [P?] [Story] Description`

- **(maintainer)**: Only the maintainer can do this task. `/speckit-implement` stops and hands it
  over.

## Phase 1: Setup

- [X] T001 Copy every Markdown and PNG file under `docs/` to the integration test output
- [X] T002 [P] Add `docs/theme-song/Cinzel.ttf` and `Cinzel-OFL.txt`, unchanged from DnDGen/.github

## Phase 2: Tests

- [ ] T003 (maintainer) Approve the test list. It is DnDGen.Stress's approved list, with RollGen's
  names:
  - Record: `HasCorrectSectionsInOrder`; `SunoField_FitsSunoLimit` for Title, Style, and Lyrics;
    `Sources_HasCorrectTable`; `Sources_EveryQuotedPhraseIsStillInTheReadme`;
    `AlbumArt_LinksToTheSong`; `AlbumArt_ImageExists`.
  - README: `ThemeSong_HasBadgeBesideTheBuildBadge`; `ThemeSong_HasSectionAtTheEnd`;
    `ThemeSong_LinksToTheRecord`; `ThemeSong_LinksToTheSameSongAsTheRecord`;
    `DocumentLinks_AreAbsolute`.
- [X] T004 Write the record's tests in `DnDGen.RollGen.Tests.Integration/Docs/ThemeSong/ReadmeTests.cs`
- [X] T005 Write the README's theme song tests in `DnDGen.RollGen.Tests.Integration/ReadmeTests.cs`,
  and see them fail against today's README

## Phase 3: The Song

- [X] T006 [US3] Draft the Title, Style, and Lyrics from the README, and their Sources
- [ ] T007 (maintainer) Review the draft lyrics and style before anything is generated
- [X] T008 [US3] Draw the album art, recoloured from the main theme's cover, and render it
- [ ] T009 (maintainer) Approve the album art
- [ ] T010 (maintainer) Generate drafts in Suno, each Sampling the main theme's "Rogante
  Marendor" snippet (3:00.0 to 3:17.1) with Variety all the way down; pick and publish the take
- [ ] T011 (maintainer) Add the Suno link to the record (the cover and Listen on Suno) and the
  README (the badge and the Theme Song section), and record what was learned in Suno Notes

## Phase 4: The README

- [X] T012 [US1] Add the Theme Song section to the end of the README, linking to the record
- [ ] T013 (maintainer) Open the DnDGen/.github pull request from `dndgen-github.patch`, with the
  Suno link filled in

## Phase 5: Polish

- [ ] T014 Once T011 is done, run every test project and confirm coverage of `DnDGen.RollGen.dll`
  is still 100% line and branch
