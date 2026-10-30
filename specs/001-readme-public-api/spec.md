# Feature Specification: README Documents the Public API

**Feature Branch**: `001-readme-public-api`

**Created**: 2026-10-30

**Status**: Draft

**Input**: User description: "Add README-level document tests, with DnDGen.Stress as the example."
DnDGen.Stress 5.0.4 (DnDGen/DnDGen.Stress#56) added tests that its README documents every public
member, because the README is packed into the NuGet package and is the only documentation a
consumer sees (DnDGen constitution, README as contract). RollGen gets the same, for its own public
API.

## Clarifications

### Session 2026-10-30

These follow DnDGen.Stress's answers to the same questions (its spec 002), since nothing in RollGen
calls for different ones. The maintainer hasn't reviewed them yet.

- Q: Which public members must the README document? → A: Every public member of every public type
  in `DnDGen.RollGen.dll`: constructors, methods, properties, fields, and constants, as RollGen's
  constitution lists them.
- Q: Should the tests pin the README's overall structure? → A: No. The tests check only what the
  contract requires, so restructuring the README stays free.
- Q: RollGen has no options class. Where do its defaults live? → A: In optional parameters, such as
  `lenient = false` and `includeExplode = true`. Each method is documented by its full signature,
  default values included, so a changed default fails the same way a changed parameter does.

## User Scenarios & Testing *(mandatory)*

Two people rely on the README: the **consumer**, who reads it to learn what RollGen does, and the
**maintainer**, who changes RollGen and needs to know when the README has fallen behind.

### User Story 1 - A new or changed member can't go undocumented (Priority: P1)

As the maintainer, if I add, remove, or re-shape a public member of RollGen (a constructor,
method, property, field, or constant, including a parameter's type, name, or default) and don't
update the README, I expect the build to fail, and the failure to name exactly which member the
README is missing or has wrong.

**Why this priority**: Members are what consumers call. An undocumented one is invisible to them,
and a wrongly documented default is believed. Nothing catches either today.

**Independent Test**: Change `ContainsRoll`'s `lenient` default to `true` without touching the
README: one test fails, listing `ContainsRoll(string expression, bool lenient = true)` as
undocumented and `ContainsRoll(string expression, bool lenient = false)` as documented but missing.

**Acceptance Scenarios**:

1. **Given** every public member is documented, **When** the tests run, **Then** they pass.
2. **Given** a public member or overload that the README doesn't document, **When** the tests run,
   **Then** a test fails and lists every undocumented one, methods and constructors by their full
   signature.
3. **Given** the README documents a member or overload that no longer exists, **When** the tests
   run, **Then** a test fails and lists every documented one that doesn't exist.
4. **Given** a public type is added, **When** the tests run, **Then** its members are checked too,
   without anyone adding the type to a list.

### Edge Cases

- **Overloads with the same parameter names**: `d(int die)` and `d(string die)`, and `AsSum()` and
  `AsSum<T>()`, differ only by parameter type or type parameter. Each overload is its own
  documented item, told apart by its full signature.
- **Grouped rows**: Related members, such as `d2()` through `d20()`, may share one table row. Every
  code span in the row's first cell counts as documented.
- **Interface parameter names**: `Dice` is an interface, so its parameter names are the interface's
  (`str`, `openexpr`), which a consumer sees in named arguments, not the implementation's.
- **Code samples**: A name that appears only inside a code sample doesn't count as documented.
- **Inherited members**: Members every .NET object has (`ToString`, `Equals`, `GetHashCode`,
  `GetType`) aren't part of RollGen's API and aren't required.
- **Restructuring**: Adding, renaming, or reordering README sections fails no new test, as long as
  every member is still documented.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The tests MUST fail when a public member of a public RollGen type, including each
  overload, isn't documented in a README table outside a code sample, and MUST list every one that
  isn't.
- **FR-002**: The tests MUST fail when the README documents a member that doesn't exist, and MUST
  list every one that doesn't.
- **FR-003**: Methods and constructors MUST be documented by their full signature: generic type
  parameters, and each parameter's type, name, `params` modifier, and default value.
- **FR-004**: "Public API" MUST mean every public constructor, method, property, field, and
  constant of every public type in `DnDGen.RollGen.dll`, found by reflection. Members inherited
  from `object` are excluded.
- **FR-005**: The README MUST be brought up to date in the same change, so that every new test
  passes, and the existing code samples and their tests MUST keep passing.
- **FR-006**: The new tests MUST sit in the integration `ReadmeTests`, beside the tests of the
  README's code samples, one test file per file under test.
- **FR-007**: The tests MUST NOT pin the README's overall structure.

### Key Entities

- **Documented item**: A member as the README names it in the first column of a Constructor,
  Method, or Property table. A method or constructor is written as its signature, such as
  `ContainsRoll(string expression, bool lenient = false)`; any other member by its name.
- **Public API**: FR-004.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: With the README updated, every new test passes, and the README documents 100% of
  the public API.
- **SC-002**: Removing any single documented item from the README makes exactly the test for that
  kind of item fail, naming that item.
- **SC-003**: Changing any single default makes exactly one test fail, naming the method.
- **SC-004**: Line and branch coverage of `DnDGen.RollGen.dll` stays at 100% (II.2.c (RollGen)).
- **SC-005**: A maintainer reading a failure knows what to change in the README without opening
  the test code (II.3).

## Assumptions

- Stress writes members as `Name(parameter, names)`. RollGen's overloads share parameter names, so
  its signatures add the parameter types and defaults. This is RollGen's own need, not a new
  DnDGen pattern (I.2).
- This is a tests-and-documentation change: no code in the package changes. It ships with whichever
  release the branch merges in, as the README change in the package.
- Today's README documents RollGen through its code samples only, so this change adds a Reference
  section with a table per public type.
- Other parts of the README contract, such as the expression syntax and the .NET version each
  major version requires, are already in the README and are out of scope for these tests.
