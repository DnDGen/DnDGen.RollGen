# Implementation Plan: README Documents the Public API

**Branch**: `001-readme-public-api` | **Date**: 2026-10-30 | **Spec**: [spec.md](spec.md)

## Summary

Add tests to the integration `ReadmeTests` that compare, by reflection, every public member of
every public type in `DnDGen.RollGen.dll` with the members listed in the README's Constructor,
Method, and Property tables, then add a Reference section to the README that makes them pass. The
tests follow DnDGen.Stress's `ReadmeTests` (DnDGen/DnDGen.Stress#56), with two RollGen-specific
changes: members are written as full signatures, and the public types are found rather than
listed.

## Technical Context

**Language/Version**: C# on `net10.0`

**Primary Dependencies**: NUnit 5 (tests only)

**Testing**: `DnDGen.RollGen.Tests.Integration`. This feature copies `README.md` to its output,
as DnDGen.Stress's test project does.

**Project Type**: NuGet class library

**Constraints**: The README is packed into the package, so it reads well on NuGet as well as
GitHub.

## Constitution Check

| Label | Check | Result |
|---|---|---|
| I.1 | The tests copy DnDGen.Stress's `ReadmeTests` helpers (`GetLinesOutsideCodeBlocks`, `GetTableRows`, `WriteMember`) and its table kinds (Constructor, Method, Property). | Pass |
| I.2 | Full signatures and found types are needed by RollGen's overloads and seven public types; they don't replace Stress's pattern, and Stress needs neither. | Pass |
| I.3 | No comments added. | Pass |
| II | Tests are written first and seen to fail against today's README (T002), before the README changes (T003). | Pass |
| II.1.c | Constructors, methods, and properties each get their own case. | Pass |
| II.2.a | The whole public API is checked, not a sample. | Pass |
| II.2.c (RollGen) | No package code changes, so coverage stays at 100%. | Pass |
| II.3.a | Each test compares the whole documented list with the whole public list, so a failure shows every missing and extra item at once. | Pass |
| II.3.c | The tests read only files in the build output, and nothing depends on the machine. | Pass |
| III.1.c | The README gains a Reference section; existing samples and notes are unchanged. | Pass |
| README as contract | This feature is the contract's test. | Pass |
| Versioning | PATCH-level: README and tests only. | Pass |

## Decisions

- **D1: Signatures.** `WriteMember` writes `Name<T>(type name = default, ...)`, with C# keyword type
  names (`int`, `string`, `double`, `bool`, `char?`, `string[]`), `params` where present, and
  defaults as C# literals (`"{"`, `'\\'`, `null`, `false`, `0.5`). Constructors use the type name.
- **D2: Found types.** `typeof(Dice).Assembly.GetExportedTypes()` gives the public types, so a new
  type's members are checked without editing the tests (US1, scenario 4).
- **D3: Grouped rows.** Every code span in a row's first cell is a documented item, so the eight
  die shortcuts and each operator's three overloads can share a row.
- **D4: Constants are properties.** As in Stress, fields and constants are listed in Property
  tables, beside read-only properties.

## Project Structure

```text
README.md                                                 # Reference section added
DnDGen.RollGen.Tests.Integration/
├── DnDGen.RollGen.Tests.Integration.csproj               # copies README.md to the output
└── ReadmeTests.cs                                        # document tests added
```
