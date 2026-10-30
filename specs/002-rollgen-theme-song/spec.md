# Feature Specification: DnDGen.RollGen Theme Song

**Feature Branch**: `002-rollgen-theme-song`

**Created**: 2026-10-30

**Status**: Draft

**Input**: User description: "Add a RollGen theme song, following the rules in DnDGen/.github, with
DnDGen.Stress as the example." DnDGen/DnDGen.RollGen#106 (Suno Song).

## Clarifications

### Session 2026-10-30

Answered from DnDGen/.github's theme-song README and DnDGen.Stress's spec 001, which settled the
same questions. The maintainer hasn't reviewed them yet.

- Q: Which genre? → A: A tavern gambling and drinking song, the area the Soundtrack Plan gives
  RollGen.
- Q: Does it carry a leitmotif? → A: Yes. RollGen is a generator, so its song brings back its
  leitmotif from the main theme, *Grollen*: German for a rumble, or roll, of thunder.
- Q: Where do the lyrics come from? → A: RollGen's README, sung as words, never as code: no method
  calls, property names, commands, or formulas. The leitmotif and its thunder are the only lines not
  from the README.
- Q: Where does the song live? → A: `docs/theme-song/README.md`, with its album art, sources, and
  update procedure, following DnDGen.Stress's record and tests.
- Q: How are the first drafts made? → A: Fresh generations that Sample the main theme's "Rogante
  Marendor" snippet, 3:00.0 to 3:17.1. The take that merges is v1.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - A listener finds and hears the song (Priority: P1)

As someone reading RollGen's README, on GitHub or NuGet, I expect a Theme Song badge beside the
build badge and a Theme Song section at the end, both linking to the song on Suno, and the section
linking to the song's record.

**Independent Test**: Open the README: the badge and section are there, and every link works from
NuGet, so none is relative.

### User Story 2 - The song stays in step with the README (Priority: P1)

As the maintainer, if a README change rewords or removes a phrase the song quotes, I expect the
build to fail and list each phrase, and the record to tell me how to update the song.

**Independent Test**: Reword "Exploding dice could roll forever" in the README: one test fails,
naming that phrase.

### User Story 3 - The record is complete and fits Suno (Priority: P2)

As the maintainer making the song in Suno, I expect the record's Title, Style, and Lyrics to each
be one code block within Suno's limits (80, 1,000, and 5,000 characters), every lyric line to have
a source, and the album art to exist and link to the same song.

### Edge Cases

- **Before the song is generated**: There is no Suno link yet, so the link tests fail until the
  maintainer generates the song and adds it. That is the signal that the feature isn't done.
- **Repeated lines and responses**: A line sung more than once needs one Sources row, where it is
  first sung. A line wholly in parentheses is a response, and needs none.
- **Not from the README**: A Sources row starting "Not from the README:" quotes nothing, and is
  allowed only for the leitmotif and its thunder.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The record MUST have, in order, the sections Updating, Title, Style, Lyrics, Sources,
  Suno Notes, and Album Art, none empty.
- **FR-002**: Title, Style, and Lyrics MUST each be one code block within Suno's limits.
- **FR-003**: The Sources table MUST have one row per distinct lyric line, in the order first
  sung, and every phrase it quotes MUST be in the README.
- **FR-004**: The record and the README MUST link to the same, single song on Suno, and the album
  art MUST exist and link to it.
- **FR-005**: The README MUST have a Theme Song badge beside the build badge and a Theme Song
  section at the end that links to the record; every README link MUST be absolute.
- **FR-006**: The organization's Soundtrack Plan and soundtrack table MUST link to the song, by
  the maintainer's own pull request to DnDGen/.github.

## Success Criteria *(mandatory)*

- **SC-001**: Every new test passes once the maintainer adds the Suno link.
- **SC-002**: Line and branch coverage of `DnDGen.RollGen.dll` stays at 100% (II.2.c (RollGen)).

## Assumptions

- The tests are DnDGen.Stress's record and README tests, unchanged but for names (I.1).
- No package code changes: the README change ships in the package, so it is a PATCH.
