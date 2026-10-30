# Implementation Plan: DnDGen.RollGen Theme Song

**Branch**: `002-rollgen-theme-song` | **Date**: 2026-10-30 | **Spec**: [spec.md](spec.md)

## Summary

Record RollGen's theme song in `docs/theme-song`, the way DnDGen.Stress records its own: a README
with the Title, Style, Lyrics, Sources, update procedure, and Suno Notes; album art recoloured from
the main theme's cover; and the Cinzel typeface. The README gets a badge and a Theme Song section.
DnDGen.Stress's record and README tests come across unchanged but for names.

## Technical Context

**Language/Version**: C# on `net10.0`

**Testing**: `DnDGen.RollGen.Tests.Integration`, which copies `README.md` (feature 001) and now
every Markdown and PNG file under `docs/` to its output.

**Constraints**: Suno limits: 80 characters for the title, 1,000 for the style, 5,000 for the
lyrics. No network access in tests.

## Constitution Check

| Label | Check | Result |
|---|---|---|
| I.1 | Record, art, update procedure, and tests follow DnDGen.Stress's; the genre, leitmotif, and first-draft Sample follow DnDGen/.github's theme-song README. | Pass |
| I.3 | No code comments added. `cover.html` keeps the main theme's credit comment. | Pass |
| II | The record and README tests are written before the README changes, and fail until it and the Suno link are in place. | Pass |
| II.2 | Single-document tests: sampling and boundaries don't apply beyond the Suno limits, which are tested at their limit. | Pass |
| II.2.c (RollGen) | No package code changes, so coverage stays at 100%. | Pass |
| II.3.a | Source and link tests list every missing phrase or link at once. | Pass |
| II.3.c | The tests read only files in the build output. | Pass |
| README as contract | The song is a document derived from the README, says how it keeps in step, and is updated in the same pull request as the README. | Pass |
| Versioning | PATCH-level: documentation and tests only. | Pass |

## Project Structure

```text
README.md                                         # badge and Theme Song section
docs/theme-song/
├── README.md                                     # the song's record
├── cover.html, cover.png                         # album art
└── Cinzel.ttf, Cinzel-OFL.txt                    # typeface, unchanged from DnDGen/.github
DnDGen.RollGen.Tests.Integration/
├── DnDGen.RollGen.Tests.Integration.csproj       # copies docs/**/*.md and *.png
├── ReadmeTests.cs                                # badge, section, and link tests added
└── Docs/ThemeSong/ReadmeTests.cs                 # the record's tests
specs/002-rollgen-theme-song/dndgen-github.patch  # the maintainer's DnDGen/.github change
```
