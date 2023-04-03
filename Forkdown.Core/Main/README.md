# Overview
A Forkdown `Build` is a sequence (`BuildQueue`) of markdown documents, each being processed by a sequence of `Worker`s. The sequence is primarily determined by their interdependencies (each worker has a `RunsAfter` list of other worker types that must be run before). Each worker processes the whole `Document` ("page") of Markdown.

A sample build sequence to illustrate:
1. page1.md
   1. FirstWorker()
   2. SecondWorker()
   3. ThirdWorker()
2. page2.md
   1. FirstWorker()
   2. SecondWorker()
   3. ThirdWorker()