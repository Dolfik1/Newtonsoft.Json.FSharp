﻿module Newtonsoft.Json.FSharp.Tests.ListConverter

open Expecto

open System
open Newtonsoft.Json
open Newtonsoft.Json.FSharp

[<Tests>]
let list_tests =
  let serialise (x : _ list) =
    JsonConvert.SerializeObject(x, [| ListConverter() :> JsonConverter |])
  let deserialise (str : string) =
    JsonConvert.DeserializeObject(str, [| ListConverter() :> JsonConverter |])

  testList "can serialise & deserialise list" [
    testCase "back and forth" <| fun _ ->
      test [ListConverter()] [ 1; 2; 3 ]

    testCase "serialise to array representation" <| fun _ ->
      let str = serialise [ 1; 2; 3 ]
      Expect.equal "[1,2,3]" str "should be in array format"

    testCase "deserialise" <| fun _ ->
      let res = deserialise "[1, 2, 3]" : int list
      Expect.equal [1; 2; 3] res "should equal list"
  ]
