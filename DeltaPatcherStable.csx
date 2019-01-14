//DeltaPatcherStable script, currently fixes the controls and some collisions

EnsureDataLoaded();
ScriptMessage("DELTARUNE patcher for the Nintendo Switch\nv0.3 (stable)");

//The game will no longer request internet access when launching it :D
Data.GeneralInfo.GMS2AllowStatistics = false;

//Fix collision problems for interactable objects (quick hack which fixes every collision)
Data.GameObjects.ByName("obj_interactablesolid").ParentId = Data.GameObjects.ByName("obj_solidlong");

//Fix a few collisions being "transparent" (another ugly hack but hey)
Data.Rooms.ByName("room_krishallway").GameObjects.Add(new UndertaleRoom.GameObject(){   
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = 55, Y = 165, ScaleX = 21 });

Data.Rooms.ByName("room_torhouse").GameObjects.Add(new UndertaleRoom.GameObject(){   
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = 76, Y = 200, ScaleX = 28 });

Data.Rooms.ByName("room_dark_eyepuzzle").GameObjects.Add(new UndertaleRoom.GameObject(){  
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = -20, Y = 300, ScaleX = 70 });

Data.Rooms.ByName("room_dark_eyepuzzle").GameObjects.Add(new UndertaleRoom.GameObject(){  
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = -20, Y = 400, ScaleX = 70 });
 
//Fix wrong left stick up-down directions:

//Up pressed
Data.Scripts.ByName("up_p")?.Code.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Up held
Data.Scripts.ByName("up_h")?.Code.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Down pressed
Data.Scripts.ByName("down_p")?.Code.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Down held
Data.Scripts.ByName("down_h")?.Code.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Fix the rest of the controls!
Data.Scripts.ByName("scr_controls_default")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 40
00001: pushi.e -5
00002: pushi.e 0
00003: pop.v.i [array]input_k
00005: pushi.e 39
00006: pushi.e -5
00007: pushi.e 1
00008: pop.v.i [array]input_k
00010: pushi.e 38
00011: pushi.e -5
00012: pushi.e 2
00013: pop.v.i [array]input_k
00015: pushi.e 37
00016: pushi.e -5
00017: pushi.e 3
00018: pop.v.i [array]input_k
00020: pushi.e 90
00021: pushi.e -5
00022: pushi.e 4
00023: pop.v.i [array]input_k
00025: pushi.e 88
00026: pushi.e -5
00027: pushi.e 5
00028: pop.v.i [array]input_k
00030: pushi.e 67
00031: pushi.e -5
00032: pushi.e 6
00033: pop.v.i [array]input_k
00035: pushi.e 13
00036: pushi.e -5
00037: pushi.e 7
00038: pop.v.i [array]input_k
00040: pushi.e 16
00041: pushi.e -5
00042: pushi.e 8
00043: pop.v.i [array]input_k
00045: pushi.e 17
00046: pushi.e -5
00047: pushi.e 9
00048: pop.v.i [array]input_k
00050: pushi.e 14
00051: pushi.e -5
00052: pushi.e 0
00053: pop.v.i [array]input_g
00055: pushi.e 13
00056: pushi.e -5
00057: pushi.e 1
00058: pop.v.i [array]input_g
00060: pushi.e 15
00061: pushi.e -5
00062: pushi.e 2
00063: pop.v.i [array]input_g
00065: pushi.e 12
00066: pushi.e -5
00067: pushi.e 3
00068: pop.v.i [array]input_g
00070: pushi.e 0
00071: pushi.e -5
00072: pushi.e 4
00073: pop.v.i [array]input_g
00075: pushi.e 1 
00076: pushi.e -5
00077: pushi.e 5
00078: pop.v.i [array]input_g
00080: pushi.e 2
00081: pushi.e -5
00082: pushi.e 6
00083: pop.v.i [array]input_g
00085: pushi.e 999
00086: pushi.e -5
00087: pushi.e 7
00088: pop.v.i [array]input_g
00090: pushi.e 999
00091: pushi.e -5
00092: pushi.e 8
00093: pop.v.i [array]input_g
00095: pushi.e 999
00096: pushi.e -5
00097: pushi.e 9
00098: pop.v.i [array]input_g
", Data.Functions, Data.Variables, Data.Strings));

ScriptMessage("Done!");
