﻿[<AutoOpen>]
module Newtonsoft.Json.FSharp.TestFac

open Expecto
open FsCheck

open Newtonsoft.Json
open Newtonsoft.Json.FSharp
open System

(*
type MyArbs =
  static member SafeString () =
    Arb.filter (fun s -> s <> null) (Arb.Default.String ())
  static member NonNaN() =
    Arb.filter (fun n -> not (Double.IsNaN n)) (Arb.Default.Float ())
let internal noNulls =
  { FsCheck.Config.Default with Arbitrary = [ typeof<MyArbs> ] }
*)

let deserialise<'T> (converters : JsonConverter list) data =
  let converters = converters |> List.toArray
  JsonConvert.DeserializeObject<'T> (data, converters)

let test (serialisers : 'a list when 'a :> JsonConverter) a =
  let converters = serialisers |> List.map (fun x -> x :> JsonConverter) |> List.toArray
  let serialised = JsonConvert.SerializeObject(a, Formatting.Indented, converters)
  let deserialised = JsonConvert.DeserializeObject(serialised, converters)
  Expect.equal deserialised a "deserialized should equal a"

let test' (serialiser : #JsonConverter) a =
  let serialisers = [ serialiser ]
  test serialisers a

// let testProp desc f = testPropertyWithConfig noNulls desc f
