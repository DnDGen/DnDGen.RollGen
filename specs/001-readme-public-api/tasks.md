---

description: "Task list for README public-API tests"
---

# Tasks: README Documents the Public API

**Input**: Design documents from `specs/001-readme-public-api/`

**Prerequisites**: [plan.md](plan.md), [spec.md](spec.md)

**Tests**: The tests are the feature. They are written first and seen to fail for the expected
reason before the README changes that make them pass (II).

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1)
- **(maintainer)**: Only the maintainer can do this task. `/speckit-implement` stops and hands it
  over.

## Phase 1: Setup

- [X] T001 Copy `README.md` to the output of `DnDGen.RollGen.Tests.Integration`, as DnDGen.Stress's
  test project does

## Phase 2: User Story 1 - A new or changed member can't go undocumented (Priority: P1) 🎯 MVP

- [X] T002 (maintainer) Approve the test list. It is DnDGen.Stress's approved list for the same
  feature, without the option cases, since RollGen's defaults are in its signatures:
  1. `Constructors_AreAllDocumented`: if I add, remove, or change a public constructor, I expect
     the README's Constructor tables to list exactly the constructors there are, and I'll confirm
     that by comparing the two lists, missing and extra together.
  2. `Methods_AreAllDocumented`: the same, for every public method, including its parameter types,
     names, and defaults, against the Method tables.
  3. `Properties_AreAllDocumented`: the same, for public fields, constants, and properties,
     against the Property tables.
- [X] T003 [US1] Write the three tests in `DnDGen.RollGen.Tests.Integration/ReadmeTests.cs` and see
  them fail against today's README, which has no tables
- [X] T004 [US1] Add a Reference section to `README.md`, with Constructor, Method, and Property
  tables for `Dice`, `PartialRoll`, `RollHelper`, `Limits`, `Utils`, `DiceFactory`, and
  `RollGenModuleLoader`, until the tests pass

## Phase 3: Polish

- [X] T005 Run every test project; confirm coverage of `DnDGen.RollGen.dll` is still 100% line
  and branch
- [X] T006 Temporarily change one default (`ContainsRoll`'s `lenient`) and remove one README row,
  and confirm each fails only the expected test, naming the member; then revert
