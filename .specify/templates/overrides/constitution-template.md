# [PROJECT_NAME] Constitution
<!-- Example: DnDGen.Stress Constitution -->

> **Read the [DnDGen constitution](https://github.com/DnDGen/.github/blob/main/constitution/CONSTITUTION.md)
> first, in full, every time this file is read.** Fetch it from
> https://raw.githubusercontent.com/DnDGen/.github/main/constitution/CONSTITUTION.md rather than
> relying on memory or an earlier copy. Its principles, constraints, workflow, and governance apply
> to this repository, and every plan, task list, analysis, and review is checked against it as well
> as against this file. This file adds only what is specific to this repository, under the DnDGen
> constitution's labels. If the DnDGen constitution can't be read, stop and say so: don't continue
> from this file alone.
<!-- Keep the pointer above exactly as written: it is what makes every Spec Kit command read the
DnDGen constitution. See https://github.com/DnDGen/.github/tree/main/constitution -->

## Core Principles
<!-- Only additions to the DnDGen constitution's principles, each under the label it extends, such
as **II.2.c (Stress)**. Never a new principle: a new principle is an amendment to the DnDGen
constitution. Write "No additions." under a principle with none. -->

### I. Consistency Is King

[CONSISTENCY_ADDITIONS]
<!-- Example: **I.2 (Stress)** with an example from this repository of a change that is not a style
fix. -->

### II. Tests Are the Spec

**II.2.c ([SHORT_NAME])** [COVERAGE_MEASURE]
<!-- Example: Coverage is the line and branch coverage of `DnDGen.Stress.dll`. It is at 100%, so it
stays there. -->

[TESTS_ADDITIONS]
<!-- Example: **II.3.c (Stress)** with an example from this repository of a test kept independent of
the machine. -->

### III. Leave It Better Than You Found It

[IMPROVEMENT_ADDITIONS]

## Technology & Release Constraints

- **Stack**: [STACK]
  <!-- Example: C# on the latest .NET LTS, currently `net10.0`. NUnit, with Moq for test doubles. -->
- **Versioning and release**: [VERSIONING]
  <!-- A NuGet package: "A NuGet package, versioned and released as the DnDGen constitution
  describes.", then any further MAJOR changes and the pipeline that builds, tags, and publishes it.
  Anything else: how this repository is versioned and released. -->
- **README as contract**: [README_CONTRACT]
  <!-- A NuGet package: every public type, member, and option its README must document, and the
  runtime and dependency versions it states. Every repository: where its theme song is. -->

## Governance

This repository follows the DnDGen constitution's Governance. The additions in this file are
amended by pull request to this repository and versioned the same way.

**Version**: [CONSTITUTION_VERSION] | **Ratified**: [RATIFICATION_DATE] | **Last Amended**: [LAST_AMENDED_DATE]
<!-- Example: Version: 2.0.0 | Ratified: 2026-10-23 | Last Amended: 2026-10-27 -->
