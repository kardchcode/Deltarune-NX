//DeltaPatcherAlpha, currently fucks up the game and lets it unplayable, also fixes the controls, the collisions and... fixes tha saveprocess?
//Unstable version, use at your own risk
EnsureDataLoaded();

ScriptMessage("DELTARUNE patcher (alpha) for the Nintendo Switch\nv0.6 (alpha)");

//STABLE Script
//Fix the collision problems in the rest of interactable objects, HOTFIX, TODO: Fix it properly (c'mon freak)
Data.GameObjects.ByName("obj_interactablesolid").ParentId = Data.GameObjects.ByName("obj_solidlong");

//Fix some collisions: TODO: properly fix the solid long collisions being ignored
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
 
//Fix left-stick up and down inverted:

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
//End of the STABLE part


//ALPHA Script
// Declaring and creating necessary variables for code-strings-functions-whatever (extra and ossafe) 
var ossafe_savedata_save = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_savedata_save") };
var ossafe_savedata_load = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_savedata_load") };
var ossafe_ini_open = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_ini_open") };
var ossafe_ini_close = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_ini_close") };
var ossafe_game_end = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_game_end") };
var ossafe_fill_rectangle = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_fill_rectangle") };  //TODO: Implement borders
var ossafe_file_text_writeln = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_writeln") };
var ossafe_file_text_write_string = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_write_string") };
var ossafe_file_text_write_real = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_write_real") };
var ossafe_file_text_readln = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_readln") };
var ossafe_file_text_read_string = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_read_string") };
var ossafe_file_text_read_real = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_read_real") };
var ossafe_file_text_open_write = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_open_write") };
var ossafe_file_text_open_read = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_open_read") };
var ossafe_file_text_eof = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_eof") };
var ossafe_file_text_close = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_text_close") };
var ossafe_file_exists = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_exists") };
var ossafe_file_delete = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_file_delete") };
var substr = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_substr") };
var strlen = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_strlen") };

// Ensure the missing GLOBAL variables
Data.Variables.EnsureDefined("osflavor", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_id", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("switchlogin", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);  //Not necessary yet but ya now...
Data.Variables.EnsureDefined("savedata", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_buffer", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_load", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_debuginfo", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("current_ini", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);

// Ensure the misssing SELF variables
Data.Variables.EnsureDefined("undefined", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemat", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemdf", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemmag", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itembolts", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazeamt", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazesize", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemboltspeed", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemspecial", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

//Ensure the missing variables
Data.Variables.EnsureDefined("os_type", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("text", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("lines", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("handle", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

// Define some LOCAL variables (divided for each script)
var var_json = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "json", Data.Strings, Data)); //ossafe_savedata_save

var var_name = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "name", Data.Strings, Data)); //ossafe_ini_open
var var_file = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "file", Data.Strings, Data)); 
var var_data = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "data", Data.Strings, Data));

var var_x1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "x1", Data.Strings, Data)); //ossafe_fill_rectangle
var var_y1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "y1", Data.Strings, Data));
var var_x2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "x2", Data.Strings, Data));
var var_y2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(4, "y2", Data.Strings, Data));
var var_temp = Data.Variables.IndexOf(Data.Variables.DefineLocal(5, "temp", Data.Strings, Data));

var var_handle = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_writeln

var var_handle1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_write_string

var var_handle2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_write_real

var var_handle3 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_readln
var var_line3 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle4 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_read_string
var var_line4 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle5 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_read_real
var var_line5 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle6 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_open_write

var var_name1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "name", Data.Strings, Data)); //ossafe_file_text_open_read
var var_file1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "file", Data.Strings, Data)); 
var var_data1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "data", Data.Strings, Data));
var var_num_lines1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(4, "num_lines", Data.Strings, Data)); 
var var_newline_pos1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(5, "newline_pos", Data.Strings, Data));
var var_nextline_pos1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(6, "nextline_pos", Data.Strings, Data));
var var_line1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(7, "line", Data.Strings, Data));
var var_lines1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(8, "lines", Data.Strings, Data));

var var_handle7 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_eof

var var_handle8 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //ossafe_file_text_close

var var_str = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "str", Data.Strings, Data)); //substr
var var_pos = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "pos", Data.Strings, Data));
var var_len = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "len", Data.Strings, Data));

// Ensure the missing functions TODO: define scripts for this functions?
Data.Functions.EnsureDefined("buffer_async_group_begin", Data.Strings);
Data.Functions.EnsureDefined("buffer_async_group_option", Data.Strings);
Data.Functions.EnsureDefined("buffer_create", Data.Strings);
Data.Functions.EnsureDefined("buffer_write", Data.Strings);
Data.Functions.EnsureDefined("buffer_get_size", Data.Strings);
Data.Functions.EnsureDefined("buffer_save_async", Data.Strings);
Data.Functions.EnsureDefined("buffer_async_group_end", Data.Strings);
Data.Functions.EnsureDefined("buffer_load_async", Data.Strings);
Data.Functions.EnsureDefined("ds_map_set", Data.Strings);
Data.Functions.EnsureDefined("ds_map_set_post", Data.Strings);
Data.Functions.EnsureDefined("ds_map_delete", Data.Strings);
Data.Functions.EnsureDefined("string_lower", Data.Strings);
Data.Functions.EnsureDefined("string_byte_length", Data.Strings);
Data.Functions.EnsureDefined("json_encode", Data.Strings);
Data.Functions.EnsureDefined("is_undefined", Data.Strings);
Data.Functions.EnsureDefined("ini_open_from_string", Data.Strings);

// Quick hot-fix for this specific undeclared strings
Data.Strings.MakeString("is_write");
Data.Strings.MakeString("line_read");
Data.Strings.MakeString("Deltarune");
Data.Strings.MakeString("showdialog");
Data.Strings.MakeString("savepadindex"); 
Data.Strings.MakeString("slottitle"); //13219
Data.Strings.MakeString("Deltarune Save Data"); //13220
Data.Strings.MakeString("subtitle"); //13221
Data.Strings.MakeString("deltarune.sav"); //13222
Data.Strings.MakeString("load in progress"); //13223
Data.Strings.MakeString("save in progress");//13224
Data.Strings.MakeString("someargument"); //13225

//Set osflavor and savedata TODO: implement obj_time_Other_72 code for obj_time to set savedata dynamically
Data.Scripts.ByName("__init_global")?.Code.Append(Assembler.Assemble(@"
pushi.e 5
pop.v.i global.osflavor
call.i ds_map_create(argc=0)
pop.v.i global.savedata
", Data.Functions, Data.Variables, Data.Strings));

// Part 1, adding new functions and scripts

strlen.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushvar.v self.argument0
00002: call.i string_length(argc=1)
00004: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(strlen);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = strlen.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("strlen"), Code = strlen });
Data.Functions.EnsureDefined("strlen", Data.Strings);

substr.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 str " + var_str + @"
.localvar 2 pos " + var_pos + @"
.localvar 3 len " + var_len + @"
00000: pushi.e -1
00001: pushi.e 0
00002: push.v [array]argument
00004: pop.v.v local.str
00006: pushi.e -1
00007: pushi.e 1
00008: push.v [array]argument
00010: pop.v.v local.pos
00012: pushloc.v local.pos
00014: pushi.e 0
00015: cmp.i.v LT
00016: bf 00028
00017: pushloc.v local.str
00019: call.i strlen(argc=1)
00021: pushi.e 1
00022: add.i.v
00023: pushloc.v local.pos
00025: add.v.v
00026: pop.v.v local.pos
00028: pushvar.v self.argument_count
00030: pushi.e 2
00031: cmp.i.v EQ
00032: bf 00045
00033: pushloc.v local.str
00035: call.i strlen(argc=1)
00037: pushloc.v local.pos
00039: sub.v.v
00040: pushi.e 1
00041: add.i.v
00042: pop.v.v local.len
00044: b 00051
00045: pushi.e -1
00046: pushi.e 2
00047: push.v [array]argument
00049: pop.v.v local.len
00051: pushloc.v local.len
00053: pushi.e 0
00054: cmp.i.v GT
00055: bf 00066
00056: pushloc.v local.len
00058: pushloc.v local.pos
00060: pushloc.v local.str
00062: call.i string_copy(argc=3)
00064: ret.v
00065: b func_end
00066: push.s """"@13225
00068: conv.s.v
00069: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(substr);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = substr.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("substr"), Code = substr });
Data.Functions.EnsureDefined("substr", Data.Strings);

//Savedata
ossafe_savedata_save.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 json " + var_json + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00006
00005: b func_end
00006: pushglb.v global.savedata_async_id
00008: pushi.e 0
00009: cmp.i.v GTE
00010: bf 00014
00011: pushi.e 0
00012: conv.i.v
00013: ret.v
00014: push.s ""Deltarune""@13216
00016: conv.s.v
00017: call.i buffer_async_group_begin(argc=1)
00019: popz.v
00020: pushi.e 0
00021: conv.i.v
00022: push.s ""showdialog""@13217
00024: conv.s.v
00025: call.i buffer_async_group_option(argc=2)
00027: popz.v
00028: pushi.e 0
00029: conv.i.v
00030: push.s ""savepadindex""@13218
00032: conv.s.v
00033: call.i buffer_async_group_option(argc=2)
00035: popz.v
00036: push.s ""Deltarune""@13216
00038: conv.s.v
00039: push.s ""slottitle""@13219
00041: conv.s.v
00042: call.i buffer_async_group_option(argc=2)
00044: popz.v
00045: push.s ""Deltarune Save Data""@13220
00047: conv.s.v
00048: push.s ""subtitle""@13221
00050: conv.s.v
00051: call.i buffer_async_group_option(argc=2)
00053: popz.v
00054: pushglb.v global.savedata
00056: call.i json_encode(argc=1)
00058: pop.v.v local.json
00060: pushi.e 1
00061: conv.i.v
00062: pushi.e 0
00063: conv.i.v
00064: pushloc.v local.json
00066: call.i string_byte_length(argc=1)
00068: pushi.e 1
00069: add.i.v
00070: call.i buffer_create(argc=3)
00072: pop.v.v global.savedata_buffer
00074: pushloc.v local.json
00076: pushi.e 11
00077: conv.i.v
00078: pushglb.v global.savedata_buffer
00080: call.i buffer_write(argc=3)
00082: popz.v
00083: pushglb.v global.savedata_buffer
00085: call.i buffer_get_size(argc=1)
00087: pushi.e 0
00088: conv.i.v
00089: push.s ""deltarune.sav""@13222
00091: conv.s.v
00092: pushglb.v global.savedata_buffer
00094: call.i buffer_save_async(argc=4)
00096: popz.v
00097: pushi.e 0
00098: pop.v.i global.savedata_async_load
00100: push.s ""save in progress""@13224
00102: pop.v.s global.savedata_debuginfo
00104: call.i buffer_async_group_end(argc=0)
00106: pop.v.v global.savedata_async_id
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_savedata_save);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_savedata_save.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_savedata_save"), Code = ossafe_savedata_save });
Data.Functions.EnsureDefined("ossafe_savedata_save", Data.Strings);

// Loads savedata
ossafe_savedata_load.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00006
00005: b func_end
00006: push.s ""Deltarune""@13216
00008: conv.s.v
00009: call.i buffer_async_group_begin(argc=1)
00011: popz.v
00012: pushi.e 0
00013: conv.i.v
00014: push.s ""showdialog""@13217
00016: conv.s.v
00017: call.i buffer_async_group_option(argc=2)
00019: popz.v
00020: pushi.e 0
00021: conv.i.v
00022: push.s ""savepadindex""@13218
00024: conv.s.v
00025: call.i buffer_async_group_option(argc=2)
00027: popz.v
00028: push.s ""Deltarune""@13216
00030: conv.s.v
00031: push.s ""slottitle""@13219
00033: conv.s.v
00034: call.i buffer_async_group_option(argc=2)
00036: popz.v
00037: push.s ""Deltarune Save Data""@13220
00039: conv.s.v
00040: push.s ""subtitle""@13221
00042: conv.s.v
00043: call.i buffer_async_group_option(argc=2)
00045: popz.v
00046: pushi.e 1
00047: conv.i.v
00048: pushi.e 1
00049: conv.i.v
00050: pushi.e 1
00051: conv.i.v
00052: call.i buffer_create(argc=3)
00054: pop.v.v global.savedata_buffer
00056: push.i 1000000
00058: conv.i.v
00059: pushi.e 0
00060: conv.i.v
00061: push.s ""deltarune.sav""@13222
00063: conv.s.v
00064: pushglb.v global.savedata_buffer
00066: call.i buffer_load_async(argc=4)
00068: popz.v
00069: pushi.e 1
00070: pop.v.i global.savedata_async_load
00072: push.s ""load in progress""@13223
00074: pop.v.s global.savedata_debuginfo
00076: call.i buffer_async_group_end(argc=0)
00078: pop.v.v global.savedata_async_id
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_savedata_load);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_savedata_load.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_savedata_load"), Code = ossafe_savedata_load });
Data.Functions.EnsureDefined("ossafe_savedata_load", Data.Strings);

//Opens ini file from savedata
ossafe_ini_open.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 name " + var_name + @"
.localvar 2 file " + var_file + @"
.localvar 3 data " + var_data + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i ini_open(argc=1)
00009: popz.v
00010: b func_end
00011: pushvar.v self.argument0
00013: call.i string_lower(argc=1)
00015: pop.v.v local.name
00017: pushloc.v local.name
00019: pop.v.v global.current_ini
00021: pushloc.v local.name
00023: pushglb.v global.savedata
00025: call.i ds_map_find_value(argc=2)
00027: pop.v.v local.file
00029: pushloc.v local.file
00031: call.i is_undefined(argc=1)
00033: conv.v.b
00034: bf 00040
00035: push.s """"@13225
00037: pop.v.s local.data
00039: b 00044
00040: pushloc.v local.file
00042: pop.v.v local.data
00044: pushloc.v local.data
00046: call.i ini_open_from_string(argc=1)
00048: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_ini_open);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_ini_open.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_ini_open"), Code = ossafe_ini_open });
Data.Functions.EnsureDefined("ossafe_ini_open", Data.Strings);

//Closes the ini file from savedata
ossafe_ini_close.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00009
00005: call.i ini_close(argc=0)
00007: ret.v
00008: b func_end
00009: pushglb.v global.current_ini
00011: call.i is_undefined(argc=1)
00013: conv.v.b
00014: not.b
00015: bf func_end
00016: call.i ini_close(argc=0)
00018: pushglb.v global.current_ini
00020: pushglb.v global.savedata
00022: call.i ds_map_set(argc=3)
00024: popz.v
00025: pushvar.v self.undefined
00027: pop.v.v global.current_ini
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_ini_close);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_ini_close.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_ini_close"), Code = ossafe_ini_close });
Data.Functions.EnsureDefined("ossafe_ini_close", Data.Strings);

//Simply ends the game and restarts it
ossafe_game_end.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00009
00005: call.i game_end(argc=0)
00007: popz.v
00008: b func_end
00009: call.i game_restart(argc=0)
00011: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_game_end);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_game_end.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_game_end"), Code = ossafe_game_end });
Data.Functions.EnsureDefined("ossafe_game_end", Data.Strings);

//Fills the black rectangle around the game in background? I'm leaving this here because I will try to add borders like undertale later on :D
ossafe_fill_rectangle.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 x1 " + var_x1 + @"
.localvar 2 y1 " + var_y1 + @"
.localvar 3 x2 " + var_x2 + @"
.localvar 4 y2 " + var_y2 + @"
.localvar 5 temp " + var_temp + @"
00000: pushvar.v self.argument0
00002: pop.v.v local.x1
00004: pushvar.v self.argument1
00006: pop.v.v local.y1
00008: pushvar.v self.argument2
00010: pop.v.v local.x2
00012: pushvar.v self.argument3
00014: pop.v.v local.y2
00016: pushloc.v local.x1
00018: pushloc.v local.x2
00020: cmp.v.v GT
00021: bf 00034
00022: pushloc.v local.x1
00024: pop.v.v local.temp
00026: pushloc.v local.x2
00028: pop.v.v local.x1
00030: pushloc.v local.temp
00032: pop.v.v local.x2
00034: pushloc.v local.y1
00036: pushloc.v local.y2
00038: cmp.v.v GT
00039: bf 00052
00040: pushloc.v local.y1
00042: pop.v.v local.temp
00044: pushloc.v local.y2
00046: pop.v.v local.y1
00048: pushloc.v local.temp
00050: pop.v.v local.y2
00052: pushvar.v self.os_type
00054: pushi.e 14
00055: cmp.i.v EQ
00056: bt 00062
00057: pushvar.v self.os_type
00059: pushi.e 12
00060: cmp.i.v EQ
00061: b 00063
00062: push.e 1
00063: bf 00076
00064: push.v local.x2
00066: push.e 1
00067: add.i.v
00068: pop.v.v local.x2
00070: push.v local.y2
00072: push.e 1
00073: add.i.v
00074: pop.v.v local.y2
00076: pushi.e 0
00077: conv.i.v
00078: pushloc.v local.y2
00080: pushloc.v local.x2
00082: pushloc.v local.y1
00084: pushloc.v local.x1
00086: call.i draw_rectangle(argc=5)
00088: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_fill_rectangle);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_fill_rectangle.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_fill_rectangle"), Code = ossafe_fill_rectangle });
Data.Functions.EnsureDefined("ossafe_fill_rectangle", Data.Strings);

//It's obvious, isn't it?
ossafe_file_text_writeln.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_writeln(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: push.s ""data""@13189
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: push.s ""file_text_writeln""@2737
00024: add.s.v
00025: push.s ""data""@13189
00027: conv.s.v
00028: pushloc.v local.handle
00030: call.i ds_map_set(argc=3)
00032: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_writeln);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_writeln.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_writeln"), Code = ossafe_file_text_writeln });
Data.Functions.EnsureDefined("ossafe_file_text_writeln", Data.Strings);

//Writes... strings to the opened file
ossafe_file_text_write_string.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle1 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00013
00005: pushvar.v self.argument1
00007: pushvar.v self.argument0
00009: call.i file_text_write_string(argc=2)
00011: ret.v
00012: b func_end
00013: pushvar.v self.argument0
00015: pop.v.v local.handle
00017: push.s ""data""@13189
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_find_value(argc=2)
00024: pushvar.v self.argument1
00026: add.v.v
00027: push.s ""data""@13189
00029: conv.s.v
00030: pushloc.v local.handle
00032: call.i ds_map_set(argc=3)
00034: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_write_string);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_write_string.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_write_string"), Code = ossafe_file_text_write_string });
Data.Functions.EnsureDefined("ossafe_file_text_write_string", Data.Strings);

//Writes real numbers to the opened file?
ossafe_file_text_write_real.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle2 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00013
00005: pushvar.v self.argument1
00007: pushvar.v self.argument0
00009: call.i file_text_write_real(argc=2)
00011: ret.v
00012: b func_end
00013: pushvar.v self.argument0
00015: pop.v.v local.handle
00017: push.s ""data""@13189
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_find_value(argc=2)
00024: pushvar.v self.argument1
00026: call.i string(argc=1)
00028: add.v.v
00029: push.s ""data""@13189
00031: conv.s.v
00032: pushloc.v local.handle
00034: call.i ds_map_set(argc=3)
00036: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_write_real);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_write_real.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_write_real"), Code = ossafe_file_text_write_real });
Data.Functions.EnsureDefined("ossafe_file_text_write_real", Data.Strings);

//...
ossafe_file_text_readln.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle3 + @"
.localvar 2 line " + var_line3 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_readln(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: pushi.e 0
00016: conv.i.v
00017: push.s ""line_read""@13215
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_set(argc=3)
00024: popz.v
00025: push.s ""line""@9883
00027: conv.s.v
00028: pushloc.v local.handle
00030: call.i ds_map_find_value(argc=2)
00032: pushi.e 1
00033: add.i.v
00034: push.s ""line""@9883
00036: conv.s.v
00037: pushloc.v local.handle
00039: call.i ds_map_set_post(argc=3)
00041: pop.v.v local.line
00043: pushloc.v local.line
00045: push.s ""num_lines""@13195
00047: conv.s.v
00048: pushloc.v local.handle
00050: call.i ds_map_find_value(argc=2)
00052: cmp.v.v GTE
00053: bf 00058
00054: push.s """"@13225
00056: conv.s.v
00057: ret.v
00058: push.s ""text""@13186
00060: conv.s.v
00061: pushloc.v local.handle
00063: call.i ds_map_find_value(argc=2)
00065: pop.v.v self.text
00067: pushi.e -1
00068: pushloc.v local.line
00070: conv.v.i
00071: push.v [array]text
00073: push.s ""file_text_writeln""@2737
00075: add.s.v
00076: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_readln);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_readln.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_readln"), Code = ossafe_file_text_readln });
Data.Functions.EnsureDefined("ossafe_file_text_readln", Data.Strings);

//...
ossafe_file_text_read_string.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle4 + @"
.localvar 2 line " + var_line4 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_read_string(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: push.s ""line_read""@13215
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00028
00024: push.s """"@13225
00026: conv.s.v
00027: ret.v
00028: push.s ""line""@9883
00030: conv.s.v
00031: pushloc.v local.handle
00033: call.i ds_map_find_value(argc=2)
00035: pop.v.v local.line
00037: pushloc.v local.line
00039: push.s ""num_lines""@13195
00041: conv.s.v
00042: pushloc.v local.handle
00044: call.i ds_map_find_value(argc=2)
00046: cmp.v.v GTE
00047: bf 00052
00048: push.s """"@13225
00050: conv.s.v
00051: ret.v
00052: pushi.e 1
00053: conv.i.v
00054: push.s ""line_read""@13215
00056: conv.s.v
00057: pushloc.v local.handle
00059: call.i ds_map_set(argc=3)
00061: popz.v
00062: push.s ""text""@13186
00064: conv.s.v
00065: pushloc.v local.handle
00067: call.i ds_map_find_value(argc=2)
00069: pop.v.v self.text
00071: pushi.e -1
00072: pushloc.v local.line
00074: conv.v.i
00075: push.v [array]text
00077: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_read_string);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_read_string.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_read_string"), Code = ossafe_file_text_read_string });
Data.Functions.EnsureDefined("ossafe_file_text_read_string", Data.Strings);

//...
ossafe_file_text_read_real.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle5 + @"
.localvar 2 line " + var_line5 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_read_real(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: push.s ""line_read""@13215
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00027
00024: pushi.e 0
00025: conv.i.v
00026: ret.v
00027: push.s ""line""@9883
00029: conv.s.v
00030: pushloc.v local.handle
00032: call.i ds_map_find_value(argc=2)
00034: pop.v.v local.line
00036: pushloc.v local.line
00038: push.s ""num_lines""@13195
00040: conv.s.v
00041: pushloc.v local.handle
00043: call.i ds_map_find_value(argc=2)
00045: cmp.v.v GTE
00046: bf 00050
00047: pushi.e 0
00048: conv.i.v
00049: ret.v
00050: pushi.e 1
00051: conv.i.v
00052: push.s ""line_read""@13215
00054: conv.s.v
00055: pushloc.v local.handle
00057: call.i ds_map_set(argc=3)
00059: popz.v
00060: push.s ""text""@13186
00062: conv.s.v
00063: pushloc.v local.handle
00065: call.i ds_map_find_value(argc=2)
00067: pop.v.v self.text
00069: pushi.e -1
00070: pushloc.v local.line
00072: conv.v.i
00073: push.v [array]text
00075: call.i real(argc=1)
00077: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_read_real);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_read_real.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_read_real"), Code = ossafe_file_text_read_real });
Data.Functions.EnsureDefined("ossafe_file_text_read_real", Data.Strings);

//...
ossafe_file_text_open_write.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle6 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_open_write(argc=1)
00009: ret.v
00010: b func_end
00011: call.i ds_map_create(argc=0)
00013: pop.v.v local.handle
00015: pushi.e 1
00016: conv.i.v
00017: push.s ""is_write""@13214
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_set(argc=3)
00024: popz.v
00025: pushvar.v self.argument0
00027: call.i string_lower(argc=1)
00029: push.s ""filename""@3399
00031: conv.s.v
00032: pushloc.v local.handle
00034: call.i ds_map_set(argc=3)
00036: popz.v
00037: push.s """"@13225
00039: conv.s.v
00040: push.s ""data""@13189
00042: conv.s.v
00043: pushloc.v local.handle
00045: call.i ds_map_set(argc=3)
00047: popz.v
00048: pushloc.v local.handle
00050: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_open_write);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_open_write.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_open_write"), Code = ossafe_file_text_open_write });
Data.Functions.EnsureDefined("ossafe_file_text_open_write", Data.Strings);

//Todo: remove the hacky hack for global.osflavor (now handled by ibj_initializer)
ossafe_file_text_open_read.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 name " + var_name1 + @"
.localvar 2 file " + var_file1 + @"
.localvar 3 data " + var_data1 + @"
.localvar 4 num_lines " + var_num_lines1 + @"
.localvar 5 newline_pos " + var_newline_pos1 + @"
.localvar 6 nextline_pos " + var_nextline_pos1 + @"
.localvar 7 line " + var_line1 + @"
.localvar 8 lines " + var_lines1 + @"
00000: pushi.e 5
00001: pop.v.i global.osflavor
00003: pushglb.v global.osflavor
00005: pushi.e 2
00006: cmp.i.v LTE
00007: bf 00014
00008: pushvar.v self.argument0
00010: call.i file_text_open_read(argc=1)
00012: ret.v
00013: b func_end
00014: pushvar.v self.argument0
00016: call.i string_lower(argc=1)
00018: pop.v.v local.name
00020: pushloc.v local.name
00022: pushglb.v global.savedata
00024: call.i ds_map_find_value(argc=2)
00026: pop.v.v local.file
00028: pushloc.v local.file
00030: call.i is_undefined(argc=1)
00032: conv.v.b
00033: bf 00037
00034: pushvar.v self.undefined
00036: ret.v
00037: pushloc.v local.file
00039: pop.v.v local.data
00041: pushi.e 0
00042: pop.v.i local.num_lines
00044: pushloc.v local.data
00046: call.i string_byte_length(argc=1)
00048: pushi.e 0
00049: cmp.i.v GT
00050: bf 00162
00051: pushloc.v local.data
00053: push.s ""string_byte_length""@13210
00055: conv.s.v
00056: call.i string_pos(argc=2)
00058: pop.v.v local.newline_pos
00060: pushloc.v local.newline_pos
00062: pushi.e 0
00063: cmp.i.v GT
00064: bf 00140
00065: pushloc.v local.newline_pos
00067: pushi.e 1
00068: add.i.v
00069: pop.v.v local.nextline_pos
00071: pushloc.v local.newline_pos
00073: pushi.e 1
00074: cmp.i.v GT
00075: bf 00088
00076: pushloc.v local.newline_pos
00078: pushi.e 1
00079: sub.i.v
00080: pushloc.v local.data
00082: call.i string_char_at(argc=2)
00084: push.s ""ds_map_create""@3380
00086: cmp.s.v EQ
00087: b 00089
00088: push.e 0
00089: bf 00096
00090: push.v local.newline_pos
00092: push.e 1
00093: sub.i.v
00094: pop.v.v local.newline_pos
00096: pushloc.v local.newline_pos
00098: pushi.e 1
00099: cmp.i.v GT
00100: bf 00114
00101: pushloc.v local.newline_pos
00103: pushi.e 1
00104: sub.i.v
00105: pushi.e 1
00106: conv.i.v
00107: pushloc.v local.data
00109: call.i substr(argc=3)
00111: pop.v.v local.line
00113: b 00118
00114: push.s """"@13225
00116: pop.v.s local.line
00118: pushloc.v local.nextline_pos
00120: pushloc.v local.data
00122: call.i strlen(argc=1)
00124: cmp.v.v LTE
00125: bf 00135
00126: pushloc.v local.nextline_pos
00128: pushloc.v local.data
00130: call.i substr(argc=2)
00132: pop.v.v local.data
00134: b 00139
00135: push.s """"@13225
00137: pop.v.s local.data
00139: b 00148
00140: pushloc.v local.data
00142: pop.v.v local.line
00144: push.s """"@13225
00146: pop.v.s local.data
00148: pushloc.v local.line
00150: pushi.e -7
00151: push.v local.num_lines
00153: dup.v 0
00154: push.e 1
00155: add.i.v
00156: pop.v.v local.num_lines
00158: conv.v.i
00159: pop.v.v [array]lines
00161: b 00044
00162: call.i ds_map_create(argc=0)
00164: pop.v.v self.handle
00166: pushi.e 0
00167: conv.i.v
00168: push.s ""is_write""@13214
00170: conv.s.v
00171: push.v self.handle
00173: call.i ds_map_set(argc=3)
00175: popz.v
00176: pushloc.v local.lines
00178: push.s ""text""@13186
00180: conv.s.v
00181: push.v self.handle
00183: call.i ds_map_set(argc=3)
00185: popz.v
00186: pushloc.v local.num_lines
00188: push.s ""num_lines""@13195
00190: conv.s.v
00191: push.v self.handle
00193: call.i ds_map_set(argc=3)
00195: popz.v
00196: pushi.e 0
00197: conv.i.v
00198: push.s ""line""@9883
00200: conv.s.v
00201: push.v self.handle
00203: call.i ds_map_set(argc=3)
00205: popz.v
00206: pushi.e 0
00207: conv.i.v
00208: push.s ""line_read""@13215
00210: conv.s.v
00211: push.v self.handle
00213: call.i ds_map_set(argc=3)
00215: popz.v
00216: push.v self.handle
00218: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_open_read);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_open_read.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_open_read"), Code = ossafe_file_text_open_read });
Data.Functions.EnsureDefined("ossafe_file_text_open_read", Data.Strings);

//...
ossafe_file_text_eof.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle7 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_eof(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: push.s ""line""@9883
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: push.s ""num_lines""@13195
00024: conv.s.v
00025: pushloc.v local.handle
00027: call.i ds_map_find_value(argc=2)
00029: cmp.v.v GTE
00030: conv.b.v
00031: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_eof);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_eof.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_eof"), Code = ossafe_file_text_eof });
Data.Functions.EnsureDefined("ossafe_file_text_eof", Data.Strings); //TODO: Function file_text_eof is already declared why the fuck do I have to set anoher one

//...
ossafe_file_text_close.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 handle " + var_handle8 + @"
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_close(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pop.v.v local.handle
00015: push.s ""is_write""@13214
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00043
00024: push.s ""data""@13189
00026: conv.s.v
00027: pushloc.v local.handle
00029: call.i ds_map_find_value(argc=2)
00031: push.s ""filename""@3399
00033: conv.s.v
00034: pushloc.v local.handle
00036: call.i ds_map_find_value(argc=2)
00038: pushglb.v global.savedata
00040: call.i ds_map_set(argc=3)
00042: popz.v
00043: pushloc.v local.handle
00045: call.i ds_map_destroy(argc=1)
00047: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_text_close);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_text_close.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_text_close"), Code = ossafe_file_text_close });
Data.Functions.EnsureDefined("ossafe_file_text_close", Data.Strings);

//...
ossafe_file_exists.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_exists(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pushglb.v global.savedata
00015: call.i ds_map_find_value(argc=2)
00017: call.i is_undefined(argc=1)
00019: conv.v.b
00020: not.b
00021: conv.b.v
00022: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_exists);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_exists.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_exists"), Code = ossafe_file_exists });
Data.Functions.EnsureDefined("ossafe_file_exists", Data.Strings);

//...
ossafe_file_delete.Append(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_delete(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: pushglb.v global.savedata
00015: call.i ds_map_find_value(argc=2)
00017: call.i is_undefined(argc=1)
00019: conv.v.b
00020: not.b
00021: bf func_end
00022: pushvar.v self.argument0
00024: pushglb.v global.savedata
00026: call.i ds_map_delete(argc=2)
00028: popz.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(ossafe_file_delete);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ossafe_file_delete.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ossafe_file_delete"), Code = ossafe_file_delete });
Data.Functions.EnsureDefined("ossafe_file_delete", Data.Strings);


//Part 2, replacing necessary file calls with the ossafe ones ;D

Data.Scripts.ByName("scr_saveprocess")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.time
00002: pop.v.v global.lastsavedtime
00004: pushglb.v global.lv
00006: pop.v.v global.lastsavedlv
00008: push.s ""filech1_""@2713
00010: pushvar.v self.argument0
00012: call.i string(argc=1)
00014: add.v.s
00015: pop.v.v self.file
00017: push.v self.file
00019: call.i ossafe_file_text_open_write(argc=1)
00021: pop.v.v self.myfileid
00023: pushglb.v global.truename
00025: push.v self.myfileid
00027: call.i ossafe_file_text_write_string(argc=2)
00029: popz.v
00030: push.v self.myfileid
00032: call.i ossafe_file_text_writeln(argc=1)
00034: popz.v
00035: pushi.e 0
00036: pop.v.i self.i
00038: push.v self.i
00040: pushi.e 6
00041: cmp.i.v LT
00042: bf 00066
00043: pushi.e -5
00044: push.v self.i
00046: conv.v.i
00047: push.v [array]othername
00049: push.v self.myfileid
00051: call.i ossafe_file_text_write_string(argc=2)
00053: popz.v
00054: push.v self.myfileid
00056: call.i ossafe_file_text_writeln(argc=1)
00058: popz.v
00059: push.v self.i
00061: pushi.e 1
00062: add.i.v
00063: pop.v.v self.i
00065: b 00038
00066: pushi.e -5
00067: pushi.e 0
00068: push.v [array]char
00070: push.v self.myfileid
00072: call.i ossafe_file_text_write_real(argc=2)
00074: popz.v
00075: push.v self.myfileid
00077: call.i ossafe_file_text_writeln(argc=1)
00079: popz.v
00080: pushi.e -5
00081: pushi.e 1
00082: push.v [array]char
00084: push.v self.myfileid
00086: call.i ossafe_file_text_write_real(argc=2)
00088: popz.v
00089: push.v self.myfileid
00091: call.i ossafe_file_text_writeln(argc=1)
00093: popz.v
00094: pushi.e -5
00095: pushi.e 2
00096: push.v [array]char
00098: push.v self.myfileid
00100: call.i ossafe_file_text_write_real(argc=2)
00102: popz.v
00103: push.v self.myfileid
00105: call.i ossafe_file_text_writeln(argc=1)
00107: popz.v
00108: pushglb.v global.gold
00110: push.v self.myfileid
00112: call.i ossafe_file_text_write_real(argc=2)
00114: popz.v
00115: push.v self.myfileid
00117: call.i ossafe_file_text_writeln(argc=1)
00119: popz.v
00120: pushglb.v global.xp
00122: push.v self.myfileid
00124: call.i ossafe_file_text_write_real(argc=2)
00126: popz.v
00127: push.v self.myfileid
00129: call.i ossafe_file_text_writeln(argc=1)
00131: popz.v
00132: pushglb.v global.lv
00134: push.v self.myfileid
00136: call.i ossafe_file_text_write_real(argc=2)
00138: popz.v
00139: push.v self.myfileid
00141: call.i ossafe_file_text_writeln(argc=1)
00143: popz.v
00144: pushglb.v global.inv
00146: push.v self.myfileid
00148: call.i ossafe_file_text_write_real(argc=2)
00150: popz.v
00151: push.v self.myfileid
00153: call.i ossafe_file_text_writeln(argc=1)
00155: popz.v
00156: pushglb.v global.invc
00158: push.v self.myfileid
00160: call.i ossafe_file_text_write_real(argc=2)
00162: popz.v
00163: push.v self.myfileid
00165: call.i ossafe_file_text_writeln(argc=1)
00167: popz.v
00168: pushglb.v global.darkzone
00170: push.v self.myfileid
00172: call.i ossafe_file_text_write_real(argc=2)
00174: popz.v
00175: push.v self.myfileid
00177: call.i ossafe_file_text_writeln(argc=1)
00179: popz.v
00180: pushi.e 0
00181: pop.v.i self.i
00183: push.v self.i
00185: pushi.e 4
00186: cmp.i.v LT
00187: bf 00610
00188: pushi.e -5
00189: push.v self.i
00191: conv.v.i
00192: push.v [array]hp
00194: push.v self.myfileid
00196: call.i ossafe_file_text_write_real(argc=2)
00198: popz.v
00199: push.v self.myfileid
00201: call.i ossafe_file_text_writeln(argc=1)
00203: popz.v
00204: pushi.e -5
00205: push.v self.i
00207: conv.v.i
00208: push.v [array]maxhp
00210: push.v self.myfileid
00212: call.i ossafe_file_text_write_real(argc=2)
00214: popz.v
00215: push.v self.myfileid
00217: call.i ossafe_file_text_writeln(argc=1)
00219: popz.v
00220: pushi.e -5
00221: push.v self.i
00223: conv.v.i
00224: push.v [array]at
00226: push.v self.myfileid
00228: call.i ossafe_file_text_write_real(argc=2)
00230: popz.v
00231: push.v self.myfileid
00233: call.i ossafe_file_text_writeln(argc=1)
00235: popz.v
00236: pushi.e -5
00237: push.v self.i
00239: conv.v.i
00240: push.v [array]df
00242: push.v self.myfileid
00244: call.i ossafe_file_text_write_real(argc=2)
00246: popz.v
00247: push.v self.myfileid
00249: call.i ossafe_file_text_writeln(argc=1)
00251: popz.v
00252: pushi.e -5
00253: push.v self.i
00255: conv.v.i
00256: push.v [array]mag
00258: push.v self.myfileid
00260: call.i ossafe_file_text_write_real(argc=2)
00262: popz.v
00263: push.v self.myfileid
00265: call.i ossafe_file_text_writeln(argc=1)
00267: popz.v
00268: pushi.e -5
00269: push.v self.i
00271: conv.v.i
00272: push.v [array]guts
00274: push.v self.myfileid
00276: call.i ossafe_file_text_write_real(argc=2)
00278: popz.v
00279: push.v self.myfileid
00281: call.i ossafe_file_text_writeln(argc=1)
00283: popz.v
00284: pushi.e -5
00285: push.v self.i
00287: conv.v.i
00288: push.v [array]charweapon
00290: push.v self.myfileid
00292: call.i ossafe_file_text_write_real(argc=2)
00294: popz.v
00295: push.v self.myfileid
00297: call.i ossafe_file_text_writeln(argc=1)
00299: popz.v
00300: pushi.e -5
00301: push.v self.i
00303: conv.v.i
00304: push.v [array]chararmor1
00306: push.v self.myfileid
00308: call.i ossafe_file_text_write_real(argc=2)
00310: popz.v
00311: push.v self.myfileid
00313: call.i ossafe_file_text_writeln(argc=1)
00315: popz.v
00316: pushi.e -5
00317: push.v self.i
00319: conv.v.i
00320: push.v [array]chararmor2
00322: push.v self.myfileid
00324: call.i ossafe_file_text_write_real(argc=2)
00326: popz.v
00327: push.v self.myfileid
00329: call.i ossafe_file_text_writeln(argc=1)
00331: popz.v
00332: pushi.e -5
00333: push.v self.i
00335: conv.v.i
00336: push.v [array]weaponstyle
00338: push.v self.myfileid
00340: call.i ossafe_file_text_write_real(argc=2)
00342: popz.v
00343: push.v self.myfileid
00345: call.i ossafe_file_text_writeln(argc=1)
00347: popz.v
00348: pushi.e 0
00349: pop.v.i self.q
00351: push.v self.q
00353: pushi.e 4
00354: cmp.i.v LT
00355: bf 00563
00356: pushi.e -5
00357: push.v self.i
00359: conv.v.i
00360: break.e -1
00361: push.i 32000
00363: mul.i.i
00364: push.v self.q
00366: conv.v.i
00367: break.e -1
00368: add.i.i
00369: push.v [array]itemat
00371: push.v self.myfileid
00373: call.i ossafe_file_text_write_real(argc=2)
00375: popz.v
00376: push.v self.myfileid
00378: call.i ossafe_file_text_writeln(argc=1)
00380: popz.v
00381: pushi.e -5
00382: push.v self.i
00384: conv.v.i
00385: break.e -1
00386: push.i 32000
00388: mul.i.i
00389: push.v self.q
00391: conv.v.i
00392: break.e -1
00393: add.i.i
00394: push.v [array]itemdf
00396: push.v self.myfileid
00398: call.i ossafe_file_text_write_real(argc=2)
00400: popz.v
00401: push.v self.myfileid
00403: call.i ossafe_file_text_writeln(argc=1)
00405: popz.v
00406: pushi.e -5
00407: push.v self.i
00409: conv.v.i
00410: break.e -1
00411: push.i 32000
00413: mul.i.i
00414: push.v self.q
00416: conv.v.i
00417: break.e -1
00418: add.i.i
00419: push.v [array]itemmag
00421: push.v self.myfileid
00423: call.i ossafe_file_text_write_real(argc=2)
00425: popz.v
00426: push.v self.myfileid
00428: call.i ossafe_file_text_writeln(argc=1)
00430: popz.v
00431: pushi.e -5
00432: push.v self.i
00434: conv.v.i
00435: break.e -1
00436: push.i 32000
00438: mul.i.i
00439: push.v self.q
00441: conv.v.i
00442: break.e -1
00443: add.i.i
00444: push.v [array]itembolts
00446: push.v self.myfileid
00448: call.i ossafe_file_text_write_real(argc=2)
00450: popz.v
00451: push.v self.myfileid
00453: call.i ossafe_file_text_writeln(argc=1)
00455: popz.v
00456: pushi.e -5
00457: push.v self.i
00459: conv.v.i
00460: break.e -1
00461: push.i 32000
00463: mul.i.i
00464: push.v self.q
00466: conv.v.i
00467: break.e -1
00468: add.i.i
00469: push.v [array]itemgrazeamt
00471: push.v self.myfileid
00473: call.i ossafe_file_text_write_real(argc=2)
00475: popz.v
00476: push.v self.myfileid
00478: call.i ossafe_file_text_writeln(argc=1)
00480: popz.v
00481: pushi.e -5
00482: push.v self.i
00484: conv.v.i
00485: break.e -1
00486: push.i 32000
00488: mul.i.i
00489: push.v self.q
00491: conv.v.i
00492: break.e -1
00493: add.i.i
00494: push.v [array]itemgrazesize
00496: push.v self.myfileid
00498: call.i ossafe_file_text_write_real(argc=2)
00500: popz.v
00501: push.v self.myfileid
00503: call.i ossafe_file_text_writeln(argc=1)
00505: popz.v
00506: pushi.e -5
00507: push.v self.i
00509: conv.v.i
00510: break.e -1
00511: push.i 32000
00513: mul.i.i
00514: push.v self.q
00516: conv.v.i
00517: break.e -1
00518: add.i.i
00519: push.v [array]itemboltspeed
00521: push.v self.myfileid
00523: call.i ossafe_file_text_write_real(argc=2)
00525: popz.v
00526: push.v self.myfileid
00528: call.i ossafe_file_text_writeln(argc=1)
00530: popz.v
00531: pushi.e -5
00532: push.v self.i
00534: conv.v.i
00535: break.e -1
00536: push.i 32000
00538: mul.i.i
00539: push.v self.q
00541: conv.v.i
00542: break.e -1
00543: add.i.i
00544: push.v [array]itemspecial
00546: push.v self.myfileid
00548: call.i ossafe_file_text_write_real(argc=2)
00550: popz.v
00551: push.v self.myfileid
00553: call.i ossafe_file_text_writeln(argc=1)
00555: popz.v
00556: push.v self.q
00558: pushi.e 1
00559: add.i.v
00560: pop.v.v self.q
00562: b 00351
00563: pushi.e 0
00564: pop.v.i self.j
00566: push.v self.j
00568: pushi.e 12
00569: cmp.i.v LT
00570: bf 00603
00571: pushi.e -5
00572: push.v self.i
00574: conv.v.i
00575: break.e -1
00576: push.i 32000
00578: mul.i.i
00579: push.v self.j
00581: conv.v.i
00582: break.e -1
00583: add.i.i
00584: push.v [array]spell
00586: push.v self.myfileid
00588: call.i ossafe_file_text_write_real(argc=2)
00590: popz.v
00591: push.v self.myfileid
00593: call.i ossafe_file_text_writeln(argc=1)
00595: popz.v
00596: push.v self.j
00598: pushi.e 1
00599: add.i.v
00600: pop.v.v self.j
00602: b 00566
00603: push.v self.i
00605: pushi.e 1
00606: add.i.v
00607: pop.v.v self.i
00609: b 00183
00610: pushglb.v global.boltspeed
00612: push.v self.myfileid
00614: call.i ossafe_file_text_write_real(argc=2)
00616: popz.v
00617: push.v self.myfileid
00619: call.i ossafe_file_text_writeln(argc=1)
00621: popz.v
00622: pushglb.v global.grazeamt
00624: push.v self.myfileid
00626: call.i ossafe_file_text_write_real(argc=2)
00628: popz.v
00629: push.v self.myfileid
00631: call.i ossafe_file_text_writeln(argc=1)
00633: popz.v
00634: pushglb.v global.grazesize
00636: push.v self.myfileid
00638: call.i ossafe_file_text_write_real(argc=2)
00640: popz.v
00641: push.v self.myfileid
00643: call.i ossafe_file_text_writeln(argc=1)
00645: popz.v
00646: pushi.e 0
00647: pop.v.i self.j
00649: push.v self.j
00651: pushi.e 13
00652: cmp.i.v LT
00653: bf 00725
00654: pushi.e -5
00655: push.v self.j
00657: conv.v.i
00658: push.v [array]item
00660: push.v self.myfileid
00662: call.i ossafe_file_text_write_real(argc=2)
00664: popz.v
00665: push.v self.myfileid
00667: call.i ossafe_file_text_writeln(argc=1)
00669: popz.v
00670: pushi.e -5
00671: push.v self.j
00673: conv.v.i
00674: push.v [array]keyitem
00676: push.v self.myfileid
00678: call.i ossafe_file_text_write_real(argc=2)
00680: popz.v
00681: push.v self.myfileid
00683: call.i ossafe_file_text_writeln(argc=1)
00685: popz.v
00686: pushi.e -5
00687: push.v self.j
00689: conv.v.i
00690: push.v [array]weapon
00692: push.v self.myfileid
00694: call.i ossafe_file_text_write_real(argc=2)
00696: popz.v
00697: push.v self.myfileid
00699: call.i ossafe_file_text_writeln(argc=1)
00701: popz.v
00702: pushi.e -5
00703: push.v self.j
00705: conv.v.i
00706: push.v [array]armor
00708: push.v self.myfileid
00710: call.i ossafe_file_text_write_real(argc=2)
00712: popz.v
00713: push.v self.myfileid
00715: call.i ossafe_file_text_writeln(argc=1)
00717: popz.v
00718: push.v self.j
00720: pushi.e 1
00721: add.i.v
00722: pop.v.v self.j
00724: b 00649
00725: pushglb.v global.tension
00727: push.v self.myfileid
00729: call.i ossafe_file_text_write_real(argc=2)
00731: popz.v
00732: push.v self.myfileid
00734: call.i ossafe_file_text_writeln(argc=1)
00736: popz.v
00737: pushglb.v global.maxtension
00739: push.v self.myfileid
00741: call.i ossafe_file_text_write_real(argc=2)
00743: popz.v
00744: push.v self.myfileid
00746: call.i ossafe_file_text_writeln(argc=1)
00748: popz.v
00749: pushglb.v global.lweapon
00751: push.v self.myfileid
00753: call.i ossafe_file_text_write_real(argc=2)
00755: popz.v
00756: push.v self.myfileid
00758: call.i ossafe_file_text_writeln(argc=1)
00760: popz.v
00761: pushglb.v global.larmor
00763: push.v self.myfileid
00765: call.i ossafe_file_text_write_real(argc=2)
00767: popz.v
00768: push.v self.myfileid
00770: call.i ossafe_file_text_writeln(argc=1)
00772: popz.v
00773: pushglb.v global.lxp
00775: push.v self.myfileid
00777: call.i ossafe_file_text_write_real(argc=2)
00779: popz.v
00780: push.v self.myfileid
00782: call.i ossafe_file_text_writeln(argc=1)
00784: popz.v
00785: pushglb.v global.llv
00787: push.v self.myfileid
00789: call.i ossafe_file_text_write_real(argc=2)
00791: popz.v
00792: push.v self.myfileid
00794: call.i ossafe_file_text_writeln(argc=1)
00796: popz.v
00797: pushglb.v global.lgold
00799: push.v self.myfileid
00801: call.i ossafe_file_text_write_real(argc=2)
00803: popz.v
00804: push.v self.myfileid
00806: call.i ossafe_file_text_writeln(argc=1)
00808: popz.v
00809: pushglb.v global.lhp
00811: push.v self.myfileid
00813: call.i ossafe_file_text_write_real(argc=2)
00815: popz.v
00816: push.v self.myfileid
00818: call.i ossafe_file_text_writeln(argc=1)
00820: popz.v
00821: pushglb.v global.lmaxhp
00823: push.v self.myfileid
00825: call.i ossafe_file_text_write_real(argc=2)
00827: popz.v
00828: push.v self.myfileid
00830: call.i ossafe_file_text_writeln(argc=1)
00832: popz.v
00833: pushglb.v global.lat
00835: push.v self.myfileid
00837: call.i ossafe_file_text_write_real(argc=2)
00839: popz.v
00840: push.v self.myfileid
00842: call.i ossafe_file_text_writeln(argc=1)
00844: popz.v
00845: pushglb.v global.ldf
00847: push.v self.myfileid
00849: call.i ossafe_file_text_write_real(argc=2)
00851: popz.v
00852: push.v self.myfileid
00854: call.i ossafe_file_text_writeln(argc=1)
00856: popz.v
00857: pushglb.v global.lwstrength
00859: push.v self.myfileid
00861: call.i ossafe_file_text_write_real(argc=2)
00863: popz.v
00864: push.v self.myfileid
00866: call.i ossafe_file_text_writeln(argc=1)
00868: popz.v
00869: pushglb.v global.ladef
00871: push.v self.myfileid
00873: call.i ossafe_file_text_write_real(argc=2)
00875: popz.v
00876: push.v self.myfileid
00878: call.i ossafe_file_text_writeln(argc=1)
00880: popz.v
00881: pushi.e 0
00882: pop.v.i self.i
00884: push.v self.i
00886: pushi.e 8
00887: cmp.i.v LT
00888: bf 00928
00889: pushi.e -5
00890: push.v self.i
00892: conv.v.i
00893: push.v [array]litem
00895: push.v self.myfileid
00897: call.i ossafe_file_text_write_real(argc=2)
00899: popz.v
00900: push.v self.myfileid
00902: call.i ossafe_file_text_writeln(argc=1)
00904: popz.v
00905: pushi.e -5
00906: push.v self.i
00908: conv.v.i
00909: push.v [array]phone
00911: push.v self.myfileid
00913: call.i ossafe_file_text_write_real(argc=2)
00915: popz.v
00916: push.v self.myfileid
00918: call.i ossafe_file_text_writeln(argc=1)
00920: popz.v
00921: push.v self.i
00923: pushi.e 1
00924: add.i.v
00925: pop.v.v self.i
00927: b 00884
00928: pushi.e 0
00929: pop.v.i self.i
00931: push.v self.i
00933: pushi.e 9999
00934: cmp.i.v LT
00935: bf 00959
00936: pushi.e -5
00937: push.v self.i
00939: conv.v.i
00940: push.v [array]flag
00942: push.v self.myfileid
00944: call.i ossafe_file_text_write_real(argc=2)
00946: popz.v
00947: push.v self.myfileid
00949: call.i ossafe_file_text_writeln(argc=1)
00951: popz.v
00952: push.v self.i
00954: pushi.e 1
00955: add.i.v
00956: pop.v.v self.i
00958: b 00931
00959: pushglb.v global.plot
00961: push.v self.myfileid
00963: call.i ossafe_file_text_write_real(argc=2)
00965: popz.v
00966: push.v self.myfileid
00968: call.i ossafe_file_text_writeln(argc=1)
00970: popz.v
00971: pushglb.v global.currentroom
00973: push.v self.myfileid
00975: call.i ossafe_file_text_write_real(argc=2)
00977: popz.v
00978: push.v self.myfileid
00980: call.i ossafe_file_text_writeln(argc=1)
00982: popz.v
00983: pushglb.v global.time
00985: push.v self.myfileid
00987: call.i ossafe_file_text_write_real(argc=2)
00989: popz.v
00990: push.v self.myfileid
00992: call.i ossafe_file_text_close(argc=1)
00994: popz.v
", Data.Functions, Data.Variables, Data.Strings));

//save replacement
Data.Scripts.ByName("scr_save")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.filechoice
00002: call.i scr_saveprocess(argc=1)
00004: popz.v
00005: pushglb.v global.filechoice
00007: pop.v.v self.filechoicebk2
00009: pushi.e 9
00010: pop.v.i global.filechoice
00012: pushi.e 9
00013: conv.i.v
00014: call.i scr_saveprocess(argc=1)
00016: popz.v
00017: push.v self.filechoicebk2
00019: pop.v.v global.filechoice
00021: push.s ""dr.ini""@2744
00023: conv.s.v
00024: call.i ossafe_ini_open(argc=1)
00026: pop.v.v self.iniwrite
00028: pushglb.v global.truename
00030: push.s ""Name""@2747
00032: conv.s.v
00033: push.s ""G""@2534
00035: pushglb.v global.filechoice
00037: call.i string(argc=1)
00039: add.v.s
00040: call.i ini_write_string(argc=3)
00042: popz.v
00043: pushglb.v global.lv
00045: push.s ""Level""@2749
00047: conv.s.v
00048: push.s ""G""@2534
00050: pushglb.v global.filechoice
00052: call.i string(argc=1)
00054: add.v.s
00055: call.i ini_write_real(argc=3)
00057: popz.v
00058: pushglb.v global.llv
00060: push.s ""Love""@2751
00062: conv.s.v
00063: push.s ""G""@2534
00065: pushglb.v global.filechoice
00067: call.i string(argc=1)
00069: add.v.s
00070: call.i ini_write_real(argc=3)
00072: popz.v
00073: pushglb.v global.time
00075: push.s ""Time""@2752
00077: conv.s.v
00078: push.s ""G""@2534
00080: pushglb.v global.filechoice
00082: call.i string(argc=1)
00084: add.v.s
00085: call.i ini_write_real(argc=3)
00087: popz.v
00088: pushvar.v self.room
00090: push.s ""Room""@2753
00092: conv.s.v
00093: push.s ""G""@2534
00095: pushglb.v global.filechoice
00097: call.i string(argc=1)
00099: add.v.s
00100: call.i ini_write_real(argc=3)
00102: popz.v
00103: pushi.e -5
00104: pushi.e 912
00105: push.v [array]flag
00107: push.s ""InitLang""@2754
00109: conv.s.v
00110: push.s ""G""@2534
00112: pushglb.v global.filechoice
00114: call.i string(argc=1)
00116: add.v.s
00117: call.i ini_write_real(argc=3)
00119: popz.v
00120: call.i ossafe_ini_close(argc=0)
00122: popz.v
00124: call.i ossafe_savedata_save(argc=0)
00126: popz.v
", Data.Functions, Data.Variables, Data.Strings));

//gml_Object_DEVICE_MENU_Other_15.gml
Data.GameObjects.ByName("DEVICE_MENU").EventHandlerFor(EventType.Other, 15, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.s ""dr.ini""@2744
00002: conv.s.v
00003: call.i ossafe_ini_open(argc=1)
00005: pop.v.v self.iniwrite
00007: push.s ""------""@3511
00009: conv.s.v
00010: push.s ""Name""@2747
00012: conv.s.v
00013: push.s ""G""@2534
00015: pushi.e -1
00016: pushi.e 2
00017: push.v [array]MENUCOORD
00019: call.i string(argc=1)
00021: add.v.s
00022: call.i ini_read_string(argc=3)
00024: pop.v.v self._NEWNAME
00026: pushi.e 0
00027: conv.i.v
00028: push.s ""Time""@2752
00030: conv.s.v
00031: push.s ""G""@2534
00033: pushi.e -1
00034: pushi.e 2
00035: push.v [array]MENUCOORD
00037: call.i string(argc=1)
00039: add.v.s
00040: call.i ini_read_real(argc=3)
00042: pop.v.v self._NEWTIME
00044: pushi.e 0
00045: conv.i.v
00046: push.s ""Room""@2753
00048: conv.s.v
00049: push.s ""G""@2534
00051: pushi.e -1
00052: pushi.e 2
00053: push.v [array]MENUCOORD
00055: call.i string(argc=1)
00057: add.v.s
00058: call.i ini_read_real(argc=3)
00060: pop.v.v self._NEWROOM
00062: pushi.e 0
00063: conv.i.v
00064: push.s ""Level""@2749
00066: conv.s.v
00067: push.s ""G""@2534
00069: pushi.e -1
00070: pushi.e 2
00071: push.v [array]MENUCOORD
00073: call.i string(argc=1)
00075: add.v.s
00076: call.i ini_read_real(argc=3)
00078: pop.v.v self._NEWLEVEL
00080: push.v self._NEWNAME
00082: push.s ""Name""@2747
00084: conv.s.v
00085: push.s ""G""@2534
00087: pushi.e -1
00088: pushi.e 3
00089: push.v [array]MENUCOORD
00091: call.i string(argc=1)
00093: add.v.s
00094: call.i ini_write_string(argc=3)
00096: popz.v
00097: push.v self._NEWTIME
00099: push.s ""Time""@2752
00101: conv.s.v
00102: push.s ""G""@2534
00104: pushi.e -1
00105: pushi.e 3
00106: push.v [array]MENUCOORD
00108: call.i string(argc=1)
00110: add.v.s
00111: call.i ini_write_real(argc=3)
00113: popz.v
00114: push.v self._NEWROOM
00116: push.s ""Room""@2753
00118: conv.s.v
00119: push.s ""G""@2534
00121: pushi.e -1
00122: pushi.e 3
00123: push.v [array]MENUCOORD
00125: call.i string(argc=1)
00127: add.v.s
00128: call.i ini_write_real(argc=3)
00130: popz.v
00131: push.v self._NEWLEVEL
00133: push.s ""Level""@2749
00135: conv.s.v
00136: push.s ""G""@2534
00138: pushi.e -1
00139: pushi.e 3
00140: push.v [array]MENUCOORD
00142: call.i string(argc=1)
00144: add.v.s
00145: call.i ini_write_real(argc=3)
00147: popz.v
00148: call.i ossafe_ini_close(argc=0)
00150: popz.v
00151: pushi.e 1
00152: pushi.e -1
00153: pushi.e -1
00154: pushi.e 3
00155: push.v [array]MENUCOORD
00157: conv.v.i
00158: pop.v.i [array]FILE
00160: pushi.e -1
00161: pushi.e -1
00162: pushi.e 2
00163: push.v [array]MENUCOORD
00165: conv.v.i
00166: push.v [array]PLACE
00168: pushi.e -1
00169: pushi.e -1
00170: pushi.e 3
00171: push.v [array]MENUCOORD
00173: conv.v.i
00174: pop.v.v [array]PLACE
00176: pushi.e -1
00177: pushi.e -1
00178: pushi.e 2
00179: push.v [array]MENUCOORD
00181: conv.v.i
00182: push.v [array]TIME
00184: pushi.e -1
00185: pushi.e -1
00186: pushi.e 3
00187: push.v [array]MENUCOORD
00189: conv.v.i
00190: pop.v.v [array]TIME
00192: pushi.e -1
00193: pushi.e -1
00194: pushi.e 2
00195: push.v [array]MENUCOORD
00197: conv.v.i
00198: push.v [array]NAME
00200: pushi.e -1
00201: pushi.e -1
00202: pushi.e 3
00203: push.v [array]MENUCOORD
00205: conv.v.i
00206: pop.v.v [array]NAME
00208: pushi.e -1
00209: pushi.e -1
00210: pushi.e 2
00211: push.v [array]MENUCOORD
00213: conv.v.i
00214: push.v [array]LEVEL
00216: pushi.e -1
00217: pushi.e -1
00218: pushi.e 3
00219: push.v [array]MENUCOORD
00221: conv.v.i
00222: pop.v.v [array]LEVEL
00224: pushi.e -1
00225: pushi.e -1
00226: pushi.e 2
00227: push.v [array]MENUCOORD
00229: conv.v.i
00230: push.v [array]TIME_STRING
00232: pushi.e -1
00233: pushi.e -1
00234: pushi.e 3
00235: push.v [array]MENUCOORD
00237: conv.v.i
00238: pop.v.v [array]TIME_STRING
00240: push.s ""filech1_""@2713
00242: pushi.e -1
00243: pushi.e 3
00244: push.v [array]MENUCOORD
00246: call.i string(argc=1)
00248: add.v.s
00249: push.s ""filech1_""@2713
00251: pushi.e -1
00252: pushi.e 2
00253: push.v [array]MENUCOORD
00255: call.i string(argc=1)
00257: add.v.s
00258: call.i file_copy(argc=2)
00260: popz.v
00261: push.s ""config_""@7035
00263: pushi.e -1
00264: pushi.e 2
00265: push.v [array]MENUCOORD
00267: call.i string(argc=1)
00269: add.v.s
00270: push.s "".ini""@7036
00272: add.s.v
00273: call.i ossafe_file_exists(argc=1)
00275: conv.v.b
00276: bf func_end
00277: push.s ""config_""@7035
00279: pushi.e -1
00280: pushi.e 3
00281: push.v [array]MENUCOORD
00283: call.i string(argc=1)
00285: add.v.s
00286: push.s "".ini""@7036
00288: add.s.v
00289: push.s ""config_""@7035
00291: pushi.e -1
00292: pushi.e 2
00293: push.v [array]MENUCOORD
00295: call.i string(argc=1)
00297: add.v.s
00298: push.s "".ini""@7036
00300: add.s.v
00301: call.i file_copy(argc=2)
00303: popz.v
", Data.Functions, Data.Variables, Data.Strings)); //TODO: create file_copy ossafe script, not really needed but anyways

//gml_Object_DEVICE_MENU_Step_0.gml
Data.GameObjects.ByName("DEVICE_MENU").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.v self.MENU_NO
00002: pushi.e 1
00003: cmp.i.v EQ
00004: bt 00020
00005: push.v self.MENU_NO
00007: pushi.e 4
00008: cmp.i.v EQ
00009: bt 00020
00010: push.v self.MENU_NO
00012: pushi.e 6
00013: cmp.i.v EQ
00014: bt 00020
00015: push.v self.MENU_NO
00017: pushi.e 7
00018: cmp.i.v EQ
00019: b 00021
00020: push.e 1
00021: bf 00880
00022: call.i left_p(argc=0)
00024: conv.v.b
00025: bf 00045
00026: pushi.e -1
00027: push.v self.MENU_NO
00029: conv.v.i
00030: push.v [array]MENUCOORD
00032: pushi.e 1
00033: cmp.i.v EQ
00034: bf 00045
00035: pushi.e 0
00036: pushi.e -1
00037: push.v self.MENU_NO
00039: conv.v.i
00040: pop.v.i [array]MENUCOORD
00042: pushi.e 1
00043: pop.v.i self.MOVENOISE
00045: call.i right_p(argc=0)
00047: conv.v.b
00048: bf 00068
00049: pushi.e -1
00050: push.v self.MENU_NO
00052: conv.v.i
00053: push.v [array]MENUCOORD
00055: pushi.e 0
00056: cmp.i.v EQ
00057: bf 00068
00058: pushi.e 1
00059: pushi.e -1
00060: push.v self.MENU_NO
00062: conv.v.i
00063: pop.v.i [array]MENUCOORD
00065: pushi.e 1
00066: pop.v.i self.MOVENOISE
00068: call.i button1_p(argc=0)
00070: conv.v.b
00071: bf 00077
00072: push.v self.ONEBUFFER
00074: pushi.e 0
00075: cmp.i.v LT
00076: b 00078
00077: push.e 0
00078: bf 00828
00079: pushi.e 2
00080: pop.v.i self.ONEBUFFER
00082: pushi.e 2
00083: pop.v.i self.TWOBUFFER
00085: pushi.e 1
00086: pop.v.i self.SELNOISE
00088: pushi.e -1
00089: push.v self.MENU_NO
00091: conv.v.i
00092: push.v [array]MENUCOORD
00094: pushi.e 0
00095: cmp.i.v EQ
00096: bf 00752
00097: push.v self.MENU_NO
00099: pushi.e 1
00100: cmp.i.v EQ
00101: bf 00292
00102: pushi.e -1
00103: pushi.e -1
00104: pushi.e 0
00105: push.v [array]MENUCOORD
00107: conv.v.i
00108: push.v [array]FILE
00110: pushi.e 1
00111: cmp.i.v EQ
00112: bf 00267
00113: pushi.e -1
00114: pushi.e 0
00115: push.v [array]MENUCOORD
00117: pop.v.v global.filechoice
00119: push.s ""DEVICE_MENU_slash_Step_0_gml_35_0""@9758
00121: conv.s.v
00122: call.i scr_84_get_lang_string(argc=1)
00124: call.i scr_windowcaption(argc=1)
00126: popz.v
00127: call.i snd_free_all(argc=0)
00129: popz.v
00130: pushi.e 139
00131: conv.i.v
00132: pushi.e 0
00133: conv.i.v
00134: pushi.e 0
00135: conv.i.v
00136: call.i instance_create(argc=3)
00138: pop.v.v self.f
00140: pushi.e 1000
00141: push.v self.f
00143: conv.v.i
00144: pop.v.i [stacktop]image_xscale
00146: pushi.e 1000
00147: push.v self.f
00149: conv.v.i
00150: pop.v.i [stacktop]image_yscale
00152: push.s ""config_""@7035
00154: pushglb.v global.filechoice
00156: call.i string(argc=1)
00158: add.v.s
00159: push.s "".ini""@7036
00161: add.s.v
00162: call.i ossafe_file_exists(argc=1)
00164: conv.v.b
00165: bf 00264
00166: push.s ""config_""@7035
00168: pushglb.v global.filechoice
00170: call.i string(argc=1)
00172: add.v.s
00173: push.s "".ini""@7036
00175: add.s.v
00176: call.i ossafe_ini_open(argc=1)
00178: popz.v
00179: pushi.e 0
00180: pop.v.i self.i
00182: push.v self.i
00184: pushi.e 10
00185: cmp.i.v LT
00186: bf 00220
00187: pushi.e -1
00188: conv.i.v
00189: push.v self.i
00191: call.i string(argc=1)
00193: push.s ""KEYBOARD_CONTROLS""@7037
00195: conv.s.v
00196: call.i ini_read_real(argc=3)
00198: pop.v.v self.readval
00200: push.v self.readval
00202: pushi.e -1
00203: cmp.i.v NEQ
00204: bf 00213
00205: push.v self.readval
00207: pushi.e -5
00208: push.v self.i
00210: conv.v.i
00211: pop.v.v [array]input_k
00213: push.v self.i
00215: pushi.e 1
00216: add.i.v
00217: pop.v.v self.i
00219: b 00182
00220: pushi.e 0
00221: pop.v.i self.i
00223: push.v self.i
00225: pushi.e 10
00226: cmp.i.v LT
00227: bf 00261
00228: pushi.e -1
00229: conv.i.v
00230: push.v self.i
00232: call.i string(argc=1)
00234: push.s ""GAMEPAD_CONTROLS""@7038
00236: conv.s.v
00237: call.i ini_read_real(argc=3)
00239: pop.v.v self.readval
00241: push.v self.readval
00243: pushi.e -1
00244: cmp.i.v NEQ
00245: bf 00254
00246: push.v self.readval
00248: pushi.e -5
00249: push.v self.i
00251: conv.v.i
00252: pop.v.v [array]input_g
00254: push.v self.i
00256: pushi.e 1
00257: add.i.v
00258: pop.v.v self.i
00260: b 00223
00261: call.i ossafe_ini_close(argc=0)
00263: popz.v
00264: call.i scr_load(argc=0)
00266: popz.v
00267: pushi.e -1
00268: pushi.e -1
00269: pushi.e 0
00270: push.v [array]MENUCOORD
00272: conv.v.i
00273: push.v [array]FILE
00275: pushi.e 0
00276: cmp.i.v EQ
00277: bf 00292
00278: pushi.e -1
00279: pushi.e 0
00280: push.v [array]MENUCOORD
00282: pop.v.v global.filechoice
00284: call.i snd_free_all(argc=0)
00286: popz.v
00287: pushi.e 1
00288: conv.i.v
00289: call.i room_goto(argc=1)
00291: popz.v
00292: push.v self.MENU_NO
00294: pushi.e 4
00295: cmp.i.v EQ
00296: bf 00498
00297: push.v self.TYPE
00299: pushi.e 0
00300: cmp.i.v EQ
00301: bf 00382
00302: push.s ""DEVICE_MENU_slash_Step_0_gml_74_0""@9760
00304: conv.s.v
00305: call.i scr_84_get_lang_string(argc=1)
00307: pop.v.v self.TEMPCOMMENT
00309: pushi.e -1
00310: pushi.e 0
00311: push.v [array]NAME
00313: pushi.e -1
00314: pushi.e 1
00315: push.v [array]NAME
00317: cmp.v.v EQ
00318: bf 00329
00319: pushi.e -1
00320: pushi.e 1
00321: push.v [array]NAME
00323: pushi.e -1
00324: pushi.e 2
00325: push.v [array]NAME
00327: cmp.v.v EQ
00328: b 00330
00329: push.e 0
00330: bf 00382
00331: pushi.e -1
00332: pushi.e 0
00333: push.v [array]TIME
00335: pushi.e -1
00336: pushi.e 1
00337: push.v [array]TIME
00339: cmp.v.v EQ
00340: bf 00351
00341: pushi.e -1
00342: pushi.e 1
00343: push.v [array]TIME
00345: pushi.e -1
00346: pushi.e 2
00347: push.v [array]TIME
00349: cmp.v.v EQ
00350: b 00352
00351: push.e 0
00352: bf 00382
00353: pushi.e -1
00354: pushi.e 0
00355: push.v [array]PLACE
00357: pushi.e -1
00358: pushi.e 1
00359: push.v [array]PLACE
00361: cmp.v.v EQ
00362: bf 00373
00363: pushi.e -1
00364: pushi.e 1
00365: push.v [array]PLACE
00367: pushi.e -1
00368: pushi.e 2
00369: push.v [array]PLACE
00371: cmp.v.v EQ
00372: b 00374
00373: push.e 0
00374: bf 00382
00375: push.s ""DEVICE_MENU_slash_Step_0_gml_77_0""@9762
00377: conv.s.v
00378: call.i scr_84_get_lang_string(argc=1)
00380: pop.v.v self.TEMPCOMMENT
00382: pushi.e 5
00383: conv.i.v
00384: call.i event_user(argc=1)
00386: popz.v
00387: push.v self.TYPE
00389: pushi.e 0
00390: cmp.i.v EQ
00391: bf 00474
00392: pushi.e -1
00393: pushi.e 0
00394: push.v [array]NAME
00396: pushi.e -1
00397: pushi.e 1
00398: push.v [array]NAME
00400: cmp.v.v EQ
00401: bf 00412
00402: pushi.e -1
00403: pushi.e 1
00404: push.v [array]NAME
00406: pushi.e -1
00407: pushi.e 2
00408: push.v [array]NAME
00410: cmp.v.v EQ
00411: b 00413
00412: push.e 0
00413: bf 00474
00414: pushi.e -1
00415: pushi.e 0
00416: push.v [array]TIME
00418: pushi.e -1
00419: pushi.e 1
00420: push.v [array]TIME
00422: cmp.v.v EQ
00423: bf 00434
00424: pushi.e -1
00425: pushi.e 1
00426: push.v [array]TIME
00428: pushi.e -1
00429: pushi.e 2
00430: push.v [array]TIME
00432: cmp.v.v EQ
00433: b 00435
00434: push.e 0
00435: bf 00474
00436: pushi.e -1
00437: pushi.e 0
00438: push.v [array]PLACE
00440: pushi.e -1
00441: pushi.e 1
00442: push.v [array]PLACE
00444: cmp.v.v EQ
00445: bf 00465
00446: pushi.e -1
00447: pushi.e 1
00448: push.v [array]PLACE
00450: pushi.e -1
00451: pushi.e 2
00452: push.v [array]PLACE
00454: cmp.v.v EQ
00455: bf 00465
00456: push.v self.TEMPCOMMENT
00458: push.s ""DEVICE_MENU_slash_Step_0_gml_86_0""@9763
00460: conv.s.v
00461: call.i scr_84_get_lang_string(argc=1)
00463: cmp.v.v NEQ
00464: b 00466
00465: push.e 0
00466: bf 00474
00467: push.s ""DEVICE_MENU_slash_Step_0_gml_86_1""@9764
00469: conv.s.v
00470: call.i scr_84_get_lang_string(argc=1)
00472: pop.v.v self.TEMPCOMMENT
00474: push.v self.TYPE
00476: pushi.e 1
00477: cmp.i.v EQ
00478: bf 00486
00479: push.s ""DEVICE_MENU_slash_Step_0_gml_91_0""@9765
00481: conv.s.v
00482: call.i scr_84_get_lang_string(argc=1)
00484: pop.v.v self.TEMPCOMMENT
00486: pushi.e 90
00487: pop.v.i self.MESSAGETIMER
00489: pushi.e 0
00490: pop.v.i self.SELNOISE
00492: pushi.e 1
00493: pop.v.i self.DEATHNOISE
00495: pushi.e 0
00496: pop.v.i self.MENU_NO
00498: push.v self.MENU_NO
00500: pushi.e 7
00501: cmp.i.v EQ
00502: bf 00733
00503: pushi.e 0
00504: pushi.e -1
00505: pushi.e -1
00506: pushi.e 5
00507: push.v [array]MENUCOORD
00509: conv.v.i
00510: pop.v.i [array]FILE
00512: push.s ""DEVICE_MENU_slash_Step_0_gml_105_0""@9766
00514: conv.s.v
00515: call.i scr_84_get_lang_string(argc=1)
00517: pushi.e -1
00518: pushi.e -1
00519: pushi.e 5
00520: push.v [array]MENUCOORD
00522: conv.v.i
00523: pop.v.v [array]NAME
00525: pushi.e 0
00526: pushi.e -1
00527: pushi.e -1
00528: pushi.e 5
00529: push.v [array]MENUCOORD
00531: conv.v.i
00532: pop.v.i [array]TIME
00534: push.s ""------------""@3497
00536: pushi.e -1
00537: pushi.e -1
00538: pushi.e 5
00539: push.v [array]MENUCOORD
00541: conv.v.i
00542: pop.v.s [array]PLACE
00544: pushi.e 0
00545: pushi.e -1
00546: pushi.e -1
00547: pushi.e 5
00548: push.v [array]MENUCOORD
00550: conv.v.i
00551: pop.v.i [array]LEVEL
00553: push.s ""--:--""@3500
00555: pushi.e -1
00556: pushi.e -1
00557: pushi.e 5
00558: push.v [array]MENUCOORD
00560: conv.v.i
00561: pop.v.s [array]TIME_STRING
00563: push.s ""filech1_""@2713
00565: pushi.e -1
00566: pushi.e 5
00567: push.v [array]MENUCOORD
00569: call.i string(argc=1)
00571: add.v.s
00572: call.i ossafe_file_delete(argc=1)
00574: popz.v
00575: push.s ""dr.ini""@2744
00577: conv.s.v
00578: call.i ossafe_ini_open(argc=1)
00580: pop.v.v self.iniwrite
00582: push.s ""[EMPTY]""@9768
00584: conv.s.v
00585: push.s ""Name""@2747
00587: conv.s.v
00588: push.s ""G""@2534
00590: pushi.e -1
00591: pushi.e 5
00592: push.v [array]MENUCOORD
00594: call.i string(argc=1)
00596: add.v.s
00597: call.i ini_write_string(argc=3)
00599: popz.v
00600: pushi.e 0
00601: conv.i.v
00602: push.s ""Level""@2749
00604: conv.s.v
00605: push.s ""G""@2534
00607: pushi.e -1
00608: pushi.e 5
00609: push.v [array]MENUCOORD
00611: call.i string(argc=1)
00613: add.v.s
00614: call.i ini_write_real(argc=3)
00616: popz.v
00617: pushi.e 0
00618: conv.i.v
00619: push.s ""Love""@2751
00621: conv.s.v
00622: push.s ""G""@2534
00624: pushi.e -1
00625: pushi.e 5
00626: push.v [array]MENUCOORD
00628: call.i string(argc=1)
00630: add.v.s
00631: call.i ini_write_real(argc=3)
00633: popz.v
00634: pushi.e 0
00635: conv.i.v
00636: push.s ""Time""@2752
00638: conv.s.v
00639: push.s ""G""@2534
00641: pushi.e -1
00642: pushi.e 5
00643: push.v [array]MENUCOORD
00645: call.i string(argc=1)
00647: add.v.s
00648: call.i ini_write_real(argc=3)
00650: popz.v
00651: pushi.e 0
00652: conv.i.v
00653: push.s ""Room""@2753
00655: conv.s.v
00656: push.s ""G""@2534
00658: pushi.e -1
00659: pushi.e 5
00660: push.v [array]MENUCOORD
00662: call.i string(argc=1)
00664: add.v.s
00665: call.i ini_write_real(argc=3)
00667: popz.v
00668: call.i ossafe_ini_close(argc=0)
00670: popz.v
00671: push.s ""config_""@7035
00673: pushi.e -1
00674: pushi.e 5
00675: push.v [array]MENUCOORD
00677: call.i string(argc=1)
00679: add.v.s
00680: push.s "".ini""@7036
00682: add.s.v
00683: call.i ossafe_file_exists(argc=1)
00685: conv.v.b
00686: bf 00702
00687: push.s ""config_""@7035
00689: pushi.e -1
00690: pushi.e 5
00691: push.v [array]MENUCOORD
00693: call.i string(argc=1)
00695: add.v.s
00696: push.s "".ini""@7036
00698: add.s.v
00699: call.i ossafe_file_delete(argc=1)
00701: popz.v
00702: push.s ""DEVICE_MENU_slash_Step_0_gml_126_0""@9769
00704: conv.s.v
00705: call.i scr_84_get_lang_string(argc=1)
00707: pop.v.v self.TEMPCOMMENT
00709: push.v self.TYPE
00711: pushi.e 1
00712: cmp.i.v EQ
00713: bf 00721
00714: push.s ""DEVICE_MENU_slash_Step_0_gml_127_0""@9770
00716: conv.s.v
00717: call.i scr_84_get_lang_string(argc=1)
00719: pop.v.v self.TEMPCOMMENT
00721: pushi.e 90
00722: pop.v.i self.MESSAGETIMER
00724: pushi.e 0
00725: pop.v.i self.SELNOISE
00727: pushi.e 1
00728: pop.v.i self.DEATHNOISE
00730: pushi.e 0
00731: pop.v.i self.MENU_NO
00733: push.v self.MENU_NO
00735: pushi.e 6
00736: cmp.i.v EQ
00737: bf 00752
00738: push.v self.THREAT
00740: pushi.e 1
00741: add.i.v
00742: pop.v.v self.THREAT
00744: pushi.e 7
00745: pop.v.i self.MENU_NO
00747: pushi.e 0
00748: pushi.e -1
00749: pushi.e 7
00750: pop.v.i [array]MENUCOORD
00752: pushi.e -1
00753: push.v self.MENU_NO
00755: conv.v.i
00756: push.v [array]MENUCOORD
00758: pushi.e 1
00759: cmp.i.v EQ
00760: bf 00828
00761: push.v self.MENU_NO
00763: pushi.e 4
00764: cmp.i.v EQ
00765: bf 00771
00766: push.v self.TYPE
00768: pushi.e 0
00769: cmp.i.v EQ
00770: b 00772
00771: push.e 0
00772: bf 00783
00773: push.s ""DEVICE_MENU_slash_Step_0_gml_149_0""@9771
00775: conv.s.v
00776: call.i scr_84_get_lang_string(argc=1)
00778: pop.v.v self.TEMPCOMMENT
00780: pushi.e 90
00781: pop.v.i self.MESSAGETIMER
00783: push.v self.MENU_NO
00785: pushi.e 6
00786: cmp.i.v EQ
00787: bt 00793
00788: push.v self.MENU_NO
00790: pushi.e 7
00791: cmp.i.v EQ
00792: b 00794
00793: push.e 1
00794: bf 00825
00795: push.v self.TYPE
00797: pushi.e 0
00798: cmp.i.v EQ
00799: bf 00825
00800: push.s ""DEVICE_MENU_slash_Step_0_gml_156_0""@9772
00802: conv.s.v
00803: call.i scr_84_get_lang_string(argc=1)
00805: pop.v.v self.TEMPCOMMENT
00807: push.v self.THREAT
00809: pushi.e 10
00810: cmp.i.v GTE
00811: bf 00822
00812: push.s ""DEVICE_MENU_slash_Step_0_gml_159_0""@9773
00814: conv.s.v
00815: call.i scr_84_get_lang_string(argc=1)
00817: pop.v.v self.TEMPCOMMENT
00819: pushi.e 0
00820: pop.v.i self.THREAT
00822: pushi.e 90
00823: pop.v.i self.MESSAGETIMER
00825: pushi.e 0
00826: pop.v.i self.MENU_NO
00828: call.i button2_p(argc=0)
00830: conv.v.b
00831: bf 00837
00832: push.v self.TWOBUFFER
00834: pushi.e 0
00835: cmp.i.v LT
00836: b 00838
00837: push.e 0
00838: bf 00880
00839: pushi.e 1
00840: pop.v.i self.ONEBUFFER
00842: pushi.e 1
00843: pop.v.i self.TWOBUFFER
00845: pushi.e 1
00846: pop.v.i self.BACKNOISE
00848: push.v self.MENU_NO
00850: pushi.e 1
00851: cmp.i.v EQ
00852: bf 00856
00853: pushi.e 0
00854: pop.v.i self.MENU_NO
00856: push.v self.MENU_NO
00858: pushi.e 4
00859: cmp.i.v EQ
00860: bf 00864
00861: pushi.e 2
00862: pop.v.i self.MENU_NO
00864: push.v self.MENU_NO
00866: pushi.e 6
00867: cmp.i.v EQ
00868: bf 00872
00869: pushi.e 5
00870: pop.v.i self.MENU_NO
00872: push.v self.MENU_NO
00874: pushi.e 7
00875: cmp.i.v EQ
00876: bf 00880
00877: pushi.e 5
00878: pop.v.i self.MENU_NO
00880: push.v self.MENU_NO
00882: pushi.e 2
00883: cmp.i.v EQ
00884: bt 00895
00885: push.v self.MENU_NO
00887: pushi.e 3
00888: cmp.i.v EQ
00889: bt 00895
00890: push.v self.MENU_NO
00892: pushi.e 5
00893: cmp.i.v EQ
00894: b 00896
00895: push.e 1
00896: bf 01346
00897: call.i down_p(argc=0)
00899: conv.v.b
00900: bf 00924
00901: pushi.e -1
00902: push.v self.MENU_NO
00904: conv.v.i
00905: push.v [array]MENUCOORD
00907: pushi.e 3
00908: cmp.i.v LT
00909: bf 00924
00910: pushi.e -1
00911: push.v self.MENU_NO
00913: conv.v.i
00914: dup.i 1
00915: push.v [array]MENUCOORD
00917: pushi.e 1
00918: add.i.v
00919: pop.i.v [array]MENUCOORD
00921: pushi.e 1
00922: pop.v.i self.MOVENOISE
00924: call.i up_p(argc=0)
00926: conv.v.b
00927: bf 00951
00928: pushi.e -1
00929: push.v self.MENU_NO
00931: conv.v.i
00932: push.v [array]MENUCOORD
00934: pushi.e 0
00935: cmp.i.v GT
00936: bf 00951
00937: pushi.e -1
00938: push.v self.MENU_NO
00940: conv.v.i
00941: dup.i 1
00942: push.v [array]MENUCOORD
00944: pushi.e 1
00945: sub.i.v
00946: pop.i.v [array]MENUCOORD
00948: pushi.e 1
00949: pop.v.i self.MOVENOISE
00951: call.i button1_p(argc=0)
00953: conv.v.b
00954: bf 00960
00955: push.v self.ONEBUFFER
00957: pushi.e 0
00958: cmp.i.v LT
00959: b 00961
00960: push.e 0
00961: bf 01303
00962: pushi.e -1
00963: push.v self.MENU_NO
00965: conv.v.i
00966: push.v [array]MENUCOORD
00968: pushi.e 3
00969: cmp.i.v LT
00970: bf 01282
00971: push.v self.MENU_NO
00973: pushi.e 3
00974: cmp.i.v EQ
00975: bf 01088
00976: pushi.e -1
00977: pushi.e 2
00978: push.v [array]MENUCOORD
00980: pushi.e -1
00981: pushi.e 3
00982: push.v [array]MENUCOORD
00984: cmp.v.v NEQ
00985: bf 01057
00986: pushi.e -1
00987: pushi.e -1
00988: push.v self.MENU_NO
00990: conv.v.i
00991: push.v [array]MENUCOORD
00993: conv.v.i
00994: push.v [array]FILE
00996: pushi.e 1
00997: cmp.i.v EQ
00998: bf 01017
00999: pushi.e 2
01000: pop.v.i self.TWOBUFFER
01002: pushi.e 2
01003: pop.v.i self.ONEBUFFER
01005: pushi.e 1
01006: pop.v.i self.SELNOISE
01008: pushi.e 0
01009: pushi.e -1
01010: pushi.e 4
01011: pop.v.i [array]MENUCOORD
01013: pushi.e 4
01014: pop.v.i self.MENU_NO
01016: b 01056
01017: push.s ""DEVICE_MENU_slash_Step_0_gml_225_0""@9774
01019: conv.s.v
01020: call.i scr_84_get_lang_string(argc=1)
01022: pop.v.v self.TEMPCOMMENT
01024: pushi.e 90
01025: pop.v.i self.MESSAGETIMER
01027: push.v self.TYPE
01029: pushi.e 1
01030: cmp.i.v EQ
01031: bf 01039
01032: push.s ""DEVICE_MENU_slash_Step_0_gml_227_0""@9775
01034: conv.s.v
01035: call.i scr_84_get_lang_string(argc=1)
01037: pop.v.v self.TEMPCOMMENT
01039: pushi.e 1
01040: pop.v.i self.DEATHNOISE
01042: pushi.e 0
01043: pop.v.i self.MENU_NO
01045: pushi.e 2
01046: pop.v.i self.ONEBUFFER
01048: pushi.e 2
01049: pop.v.i self.TWOBUFFER
01051: pushi.e 5
01052: conv.i.v
01053: call.i event_user(argc=1)
01055: popz.v
01056: b 01088
01057: push.s ""DEVICE_MENU_slash_Step_0_gml_238_0""@9776
01059: conv.s.v
01060: call.i scr_84_get_lang_string(argc=1)
01062: pop.v.v self.TEMPCOMMENT
01064: push.v self.TYPE
01066: pushi.e 1
01067: cmp.i.v EQ
01068: bf 01076
01069: push.s ""DEVICE_MENU_slash_Step_0_gml_239_0""@9777
01071: conv.s.v
01072: call.i scr_84_get_lang_string(argc=1)
01074: pop.v.v self.TEMPCOMMENT
01076: pushi.e 90
01077: pop.v.i self.MESSAGETIMER
01079: pushi.e 2
01080: pop.v.i self.TWOBUFFER
01082: pushi.e 2
01083: pop.v.i self.ONEBUFFER
01085: pushi.e 1
01086: pop.v.i self.BACKNOISE
01088: push.v self.MENU_NO
01090: pushi.e 2
01091: cmp.i.v EQ
01092: bf 01185
01093: pushi.e -1
01094: pushi.e -1
01095: push.v self.MENU_NO
01097: conv.v.i
01098: push.v [array]MENUCOORD
01100: conv.v.i
01101: push.v [array]FILE
01103: pushi.e 1
01104: cmp.i.v EQ
01105: bf 01124
01106: pushi.e 2
01107: pop.v.i self.TWOBUFFER
01109: pushi.e 2
01110: pop.v.i self.ONEBUFFER
01112: pushi.e 1
01113: pop.v.i self.SELNOISE
01115: pushi.e 0
01116: pushi.e -1
01117: pushi.e 3
01118: pop.v.i [array]MENUCOORD
01120: pushi.e 3
01121: pop.v.i self.MENU_NO
01123: b 01185
01124: push.s ""DEVICE_MENU_slash_Step_0_gml_261_0""@9778
01126: conv.s.v
01127: call.i scr_84_get_lang_string(argc=1)
01129: pop.v.v self.TEMPCOMMENT
01131: pushi.e -1
01132: pushi.e 0
01133: push.v [array]FILE
01135: pushi.e 0
01136: cmp.i.v EQ
01137: bf 01152
01138: pushi.e -1
01139: pushi.e 1
01140: push.v [array]FILE
01142: pushi.e 0
01143: cmp.i.v EQ
01144: bf 01152
01145: pushi.e -1
01146: pushi.e 2
01147: push.v [array]FILE
01149: pushi.e 0
01150: cmp.i.v EQ
01151: b 01153
01152: push.e 0
01153: bf 01161
01154: push.s ""DEVICE_MENU_slash_Step_0_gml_264_0""@9779
01156: conv.s.v
01157: call.i scr_84_get_lang_string(argc=1)
01159: pop.v.v self.TEMPCOMMENT
01161: push.v self.TYPE
01163: pushi.e 1
01164: cmp.i.v EQ
01165: bf 01173
01166: push.s ""DEVICE_MENU_slash_Step_0_gml_266_0""@9780
01168: conv.s.v
01169: call.i scr_84_get_lang_string(argc=1)
01171: pop.v.v self.TEMPCOMMENT
01173: pushi.e 90
01174: pop.v.i self.MESSAGETIMER
01176: pushi.e 1
01177: pop.v.i self.BACKNOISE
01179: pushi.e 2
01180: pop.v.i self.TWOBUFFER
01182: pushi.e 2
01183: pop.v.i self.ONEBUFFER
01185: push.v self.MENU_NO
01187: pushi.e 5
01188: cmp.i.v EQ
01189: bf 01282
01190: pushi.e -1
01191: pushi.e -1
01192: push.v self.MENU_NO
01194: conv.v.i
01195: push.v [array]MENUCOORD
01197: conv.v.i
01198: push.v [array]FILE
01200: pushi.e 1
01201: cmp.i.v EQ
01202: bf 01221
01203: pushi.e 2
01204: pop.v.i self.TWOBUFFER
01206: pushi.e 2
01207: pop.v.i self.ONEBUFFER
01209: pushi.e 1
01210: pop.v.i self.SELNOISE
01212: pushi.e 0
01213: pushi.e -1
01214: pushi.e 6
01215: pop.v.i [array]MENUCOORD
01217: pushi.e 6
01218: pop.v.i self.MENU_NO
01220: b 01282
01221: push.s ""DEVICE_MENU_slash_Step_0_gml_289_0""@9781
01223: conv.s.v
01224: call.i scr_84_get_lang_string(argc=1)
01226: pop.v.v self.TEMPCOMMENT
01228: pushi.e -1
01229: pushi.e 0
01230: push.v [array]FILE
01232: pushi.e 0
01233: cmp.i.v EQ
01234: bf 01249
01235: pushi.e -1
01236: pushi.e 1
01237: push.v [array]FILE
01239: pushi.e 0
01240: cmp.i.v EQ
01241: bf 01249
01242: pushi.e -1
01243: pushi.e 2
01244: push.v [array]FILE
01246: pushi.e 0
01247: cmp.i.v EQ
01248: b 01250
01249: push.e 0
01250: bf 01258
01251: push.s ""DEVICE_MENU_slash_Step_0_gml_292_0""@9782
01253: conv.s.v
01254: call.i scr_84_get_lang_string(argc=1)
01256: pop.v.v self.TEMPCOMMENT
01258: push.v self.TYPE
01260: pushi.e 1
01261: cmp.i.v EQ
01262: bf 01270
01263: push.s ""DEVICE_MENU_slash_Step_0_gml_294_0""@9783
01265: conv.s.v
01266: call.i scr_84_get_lang_string(argc=1)
01268: pop.v.v self.TEMPCOMMENT
01270: pushi.e 90
01271: pop.v.i self.MESSAGETIMER
01273: pushi.e 2
01274: pop.v.i self.TWOBUFFER
01276: pushi.e 2
01277: pop.v.i self.ONEBUFFER
01279: pushi.e 1
01280: pop.v.i self.BACKNOISE
01282: pushi.e -1
01283: push.v self.MENU_NO
01285: conv.v.i
01286: push.v [array]MENUCOORD
01288: pushi.e 3
01289: cmp.i.v EQ
01290: bf 01303
01291: pushi.e 2
01292: pop.v.i self.TWOBUFFER
01294: pushi.e 2
01295: pop.v.i self.ONEBUFFER
01297: pushi.e 1
01298: pop.v.i self.SELNOISE
01300: pushi.e 0
01301: pop.v.i self.MENU_NO
01303: call.i button2_p(argc=0)
01305: conv.v.b
01306: bf 01312
01307: push.v self.TWOBUFFER
01309: pushi.e 0
01310: cmp.i.v LT
01311: b 01313
01312: push.e 0
01313: bf 01346
01314: pushi.e 2
01315: pop.v.i self.TWOBUFFER
01317: pushi.e 2
01318: pop.v.i self.ONEBUFFER
01320: pushi.e 1
01321: pop.v.i self.BACKNOISE
01323: push.v self.MENU_NO
01325: pushi.e 2
01326: cmp.i.v EQ
01327: bt 01333
01328: push.v self.MENU_NO
01330: pushi.e 5
01331: cmp.i.v EQ
01332: b 01334
01333: push.e 1
01334: bf 01338
01335: pushi.e 0
01336: pop.v.i self.MENU_NO
01338: push.v self.MENU_NO
01340: pushi.e 3
01341: cmp.i.v EQ
01342: bf 01346
01343: pushi.e 2
01344: pop.v.i self.MENU_NO
01346: push.v self.MENU_NO
01348: pushi.e 0
01349: cmp.i.v EQ
01350: bf 01605
01351: call.i down_p(argc=0)
01353: conv.v.b
01354: bf 01374
01355: pushi.e -1
01356: pushi.e 0
01357: push.v [array]MENUCOORD
01359: pushi.e 3
01360: cmp.i.v LT
01361: bf 01374
01362: pushi.e -1
01363: pushi.e 0
01364: dup.i 1
01365: push.v [array]MENUCOORD
01367: pushi.e 1
01368: add.i.v
01369: pop.i.v [array]MENUCOORD
01371: pushi.e 1
01372: pop.v.i self.MOVENOISE
01374: call.i up_p(argc=0)
01376: conv.v.b
01377: bf 01409
01378: pushi.e -1
01379: pushi.e 0
01380: push.v [array]MENUCOORD
01382: pushi.e 0
01383: cmp.i.v GT
01384: bf 01409
01385: pushi.e -1
01386: pushi.e 0
01387: dup.i 1
01388: push.v [array]MENUCOORD
01390: pushi.e 1
01391: sub.i.v
01392: pop.i.v [array]MENUCOORD
01394: pushi.e -1
01395: pushi.e 0
01396: push.v [array]MENUCOORD
01398: pushi.e 3
01399: cmp.i.v EQ
01400: bf 01406
01401: pushi.e 2
01402: pushi.e -1
01403: pushi.e 0
01404: pop.v.i [array]MENUCOORD
01406: pushi.e 1
01407: pop.v.i self.MOVENOISE
01409: call.i right_p(argc=0)
01411: conv.v.b
01412: bf 01453
01413: pushi.e -1
01414: pushi.e 0
01415: push.v [array]MENUCOORD
01417: pushi.e 3
01418: cmp.i.v GTE
01419: bf 01427
01420: pushi.e -1
01421: pushi.e 0
01422: push.v [array]MENUCOORD
01424: pushi.e 5
01425: cmp.i.v LTE
01426: b 01428
01427: push.e 0
01428: bf 01453
01429: pushi.e 1
01430: pop.v.i self.MOVENOISE
01432: pushi.e -1
01433: pushi.e 0
01434: dup.i 1
01435: push.v [array]MENUCOORD
01437: pushi.e 1
01438: add.i.v
01439: pop.i.v [array]MENUCOORD
01441: pushi.e -1
01442: pushi.e 0
01443: push.v [array]MENUCOORD
01445: pushi.e 5
01446: cmp.i.v GT
01447: bf 01453
01448: pushi.e 3
01449: pushi.e -1
01450: pushi.e 0
01451: pop.v.i [array]MENUCOORD
01453: call.i left_p(argc=0)
01455: conv.v.b
01456: bf 01497
01457: pushi.e -1
01458: pushi.e 0
01459: push.v [array]MENUCOORD
01461: pushi.e 3
01462: cmp.i.v GTE
01463: bf 01471
01464: pushi.e -1
01465: pushi.e 0
01466: push.v [array]MENUCOORD
01468: pushi.e 5
01469: cmp.i.v LTE
01470: b 01472
01471: push.e 0
01472: bf 01497
01473: pushi.e 1
01474: pop.v.i self.MOVENOISE
01476: pushi.e -1
01477: pushi.e 0
01478: dup.i 1
01479: push.v [array]MENUCOORD
01481: pushi.e 1
01482: sub.i.v
01483: pop.i.v [array]MENUCOORD
01485: pushi.e -1
01486: pushi.e 0
01487: push.v [array]MENUCOORD
01489: pushi.e 3
01490: cmp.i.v LT
01491: bf 01497
01492: pushi.e 5
01493: pushi.e -1
01494: pushi.e 0
01495: pop.v.i [array]MENUCOORD
01497: call.i button1_p(argc=0)
01499: conv.v.b
01500: bf 01506
01501: push.v self.ONEBUFFER
01503: pushi.e 0
01504: cmp.i.v LT
01505: b 01507
01506: push.e 0
01507: bf 01605
01508: pushi.e -1
01509: pop.v.i self.MESSAGETIMER
01511: pushi.e -1
01512: pushi.e 0
01513: push.v [array]MENUCOORD
01515: pushi.e 2
01516: cmp.i.v LTE
01517: bf 01535
01518: pushi.e 0
01519: pushi.e -1
01520: pushi.e 1
01521: pop.v.i [array]MENUCOORD
01523: pushi.e 1
01524: pop.v.i self.ONEBUFFER
01526: pushi.e 1
01527: pop.v.i self.TWOBUFFER
01529: pushi.e 1
01530: pop.v.i self.MENU_NO
01532: pushi.e 1
01533: pop.v.i self.SELNOISE
01535: pushi.e -1
01536: pushi.e 0
01537: push.v [array]MENUCOORD
01539: pushi.e 3
01540: cmp.i.v EQ
01541: bf 01559
01542: pushi.e 0
01543: pushi.e -1
01544: pushi.e 2
01545: pop.v.i [array]MENUCOORD
01547: pushi.e 1
01548: pop.v.i self.ONEBUFFER
01550: pushi.e 1
01551: pop.v.i self.TWOBUFFER
01553: pushi.e 2
01554: pop.v.i self.MENU_NO
01556: pushi.e 1
01557: pop.v.i self.SELNOISE
01559: pushi.e -1
01560: pushi.e 0
01561: push.v [array]MENUCOORD
01563: pushi.e 4
01564: cmp.i.v EQ
01565: bf 01583
01566: pushi.e 0
01567: pushi.e -1
01568: pushi.e 5
01569: pop.v.i [array]MENUCOORD
01571: pushi.e 1
01572: pop.v.i self.ONEBUFFER
01574: pushi.e 1
01575: pop.v.i self.TWOBUFFER
01577: pushi.e 5
01578: pop.v.i self.MENU_NO
01580: pushi.e 1
01581: pop.v.i self.SELNOISE
01583: pushi.e -1
01584: pushi.e 0
01585: push.v [array]MENUCOORD
01587: pushi.e 5
01588: cmp.i.v EQ
01589: bf 01605
01590: call.i scr_change_language(argc=0)
01592: popz.v
01593: call.i scr_84_load_ini(argc=0)
01595: popz.v
01596: pushi.e 2
01597: pop.v.i self.ONEBUFFER
01599: pushi.e 2
01600: pop.v.i self.TWOBUFFER
01602: pushi.e 1
01603: pop.v.i self.SELNOISE
01605: push.v self.OBMADE
01607: pushi.e 1
01608: cmp.i.v EQ
01609: bf 01670
01610: push.v self.OB_DEPTH
01612: pushi.e 1
01613: add.i.v
01614: pop.v.v self.OB_DEPTH
01616: push.v self.obacktimer
01618: push.v self.OBM
01620: add.v.v
01621: pop.v.v self.obacktimer
01623: push.v self.obacktimer
01625: pushi.e 20
01626: cmp.i.v GTE
01627: bf 01670
01628: pushi.e 314
01629: conv.i.v
01630: pushi.e 0
01631: conv.i.v
01632: pushi.e 0
01633: conv.i.v
01634: call.i instance_create(argc=3)
01636: pop.v.v self.DV
01638: pushi.e 5
01639: push.v self.OB_DEPTH
01641: add.v.i
01642: push.v self.DV
01644: conv.v.i
01645: pop.v.v [stacktop]depth
01647: push.d 0.01
01650: push.v self.OBM
01652: mul.v.d
01653: push.v self.DV
01655: conv.v.i
01656: pop.v.v [stacktop]OBSPEED
01658: push.v self.OB_DEPTH
01660: push.i 60000
01662: cmp.i.v GTE
01663: bf 01667
01664: pushi.e 0
01665: pop.v.i self.OB_DEPTH
01667: pushi.e 0
01668: pop.v.i self.obacktimer
01670: push.v self.MOVENOISE
01672: pushi.e 1
01673: cmp.i.v EQ
01674: bf 01683
01675: pushi.e 149
01676: conv.i.v
01677: call.i snd_play(argc=1)
01679: popz.v
01680: pushi.e 0
01681: pop.v.i self.MOVENOISE
01683: push.v self.SELNOISE
01685: pushi.e 1
01686: cmp.i.v EQ
01687: bf 01696
01688: pushi.e 150
01689: conv.i.v
01690: call.i snd_play(argc=1)
01692: popz.v
01693: pushi.e 0
01694: pop.v.i self.SELNOISE
01696: push.v self.BACKNOISE
01698: pushi.e 1
01699: cmp.i.v EQ
01700: bf 01709
01701: pushi.e 79
01702: conv.i.v
01703: call.i snd_play(argc=1)
01705: popz.v
01706: pushi.e 0
01707: pop.v.i self.BACKNOISE
01709: push.v self.DEATHNOISE
01711: pushi.e 1
01712: cmp.i.v EQ
01713: bf 01722
01714: pushi.e 144
01715: conv.i.v
01716: call.i snd_play(argc=1)
01718: popz.v
01719: pushi.e 0
01720: pop.v.i self.DEATHNOISE
01722: push.v self.ONEBUFFER
01724: pushi.e 1
01725: sub.i.v
01726: pop.v.v self.ONEBUFFER
01728: push.v self.TWOBUFFER
01730: pushi.e 1
01731: sub.i.v
01732: pop.v.v self.TWOBUFFER
", Data.Functions, Data.Variables, Data.Strings));

//gml_Object_DEVICE_MENU_Create_0
Data.GameObjects.ByName("DEVICE_MENU").EventHandlerFor(EventType.Create, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 0
00001: pop.v.i self.TYPE
00003: push.s ""filech1_3""@9726
00005: conv.s.v
00006: call.i ossafe_file_exists(argc=1)
00008: conv.v.b
00009: bf 00013
00010: pushi.e 1
00011: pop.v.i self.TYPE
00013: push.s ""filech1_4""@9727
00015: conv.s.v
00016: call.i ossafe_file_exists(argc=1)
00018: conv.v.b
00019: bf 00023
00020: pushi.e 1
00021: pop.v.i self.TYPE
00023: push.s ""filech1_5""@9728
00025: conv.s.v
00026: call.i ossafe_file_exists(argc=1)
00028: conv.v.b
00029: bf 00033
00030: pushi.e 1
00031: pop.v.i self.TYPE
00033: push.v self.TYPE
00035: pushi.e 0
00036: cmp.i.v EQ
00037: bf 00065
00038: push.s ""DEVICE_MENU_slash_Create_0_gml_8_0""@9729
00040: conv.s.v
00041: call.i scr_84_get_lang_string(argc=1)
00043: call.i scr_windowcaption(argc=1)
00045: popz.v
00046: push.s ""AUDIO_DRONE.ogg""@9541
00048: conv.s.v
00049: call.i snd_init(argc=1)
00051: pushi.e -5
00052: pushi.e 0
00053: pop.v.v [array]currentsong
00055: pushi.e -5
00056: pushi.e 0
00057: push.v [array]currentsong
00059: call.i mus_loop(argc=1)
00061: pushi.e -5
00062: pushi.e 1
00063: pop.v.v [array]currentsong
00065: push.v self.TYPE
00067: pushi.e 1
00068: cmp.i.v EQ
00069: bf 00117
00070: pushi.e 138
00071: conv.i.v
00072: pushi.e 0
00073: conv.i.v
00074: pushi.e 0
00075: conv.i.v
00076: call.i instance_create(argc=3)
00078: popz.v
00079: pushi.e 1
00080: pushi.e -5
00081: pushi.e 10
00082: pop.v.i [array]tempflag
00084: push.s ""DEVICE_MENU_slash_Create_0_gml_17_0""@9730
00086: conv.s.v
00087: call.i scr_84_get_lang_string(argc=1)
00089: call.i scr_windowcaption(argc=1)
00091: popz.v
00092: push.s ""AUDIO_STORY.ogg""@9731
00094: conv.s.v
00095: call.i snd_init(argc=1)
00097: pushi.e -5
00098: pushi.e 0
00099: pop.v.v [array]currentsong
00101: push.d 0.95
00104: conv.d.v
00105: pushi.e 1
00106: conv.i.v
00107: pushi.e -5
00108: pushi.e 0
00109: push.v [array]currentsong
00111: call.i mus_loop_ext(argc=3)
00113: pushi.e -5
00114: pushi.e 1
00115: pop.v.v [array]currentsong
00117: pushi.e 0
00118: pop.v.i self.BGMADE
00120: pushi.e 0
00121: pop.v.i self.BG_ALPHA
00123: pushi.e 0
00124: pop.v.i self.BG_SINER
00126: pushi.e 0
00127: pop.v.i self.OBMADE
00129: pushi.e 0
00130: pop.v.i self.OB_DEPTH
00132: pushi.e 0
00133: pop.v.i self.obacktimer
00135: push.d 0.5
00138: pop.v.d self.OBM
00140: push.i 32768
00142: pop.v.i self.COL_A
00144: push.i 65280
00146: pop.v.i self.COL_B
00148: push.d 0.5
00151: conv.d.v
00152: push.i 16777215
00154: conv.i.v
00155: push.i 65280
00157: conv.i.v
00158: call.i merge_color(argc=3)
00160: pop.v.v self.COL_PLUS
00162: push.v self.TYPE
00164: pushi.e 1
00165: cmp.i.v EQ
00166: bf 00220
00167: pushi.e 0
00168: pop.v.i self.BGSINER
00170: pushi.e 6
00171: pop.v.i self.BGMAGNITUDE
00173: push.d 0.2
00176: conv.d.v
00177: push.i 8388608
00179: conv.i.v
00180: push.i 12632256
00182: conv.i.v
00183: call.i merge_color(argc=3)
00185: pop.v.v self.COL_A
00187: push.i 16777215
00189: pop.v.i self.COL_B
00191: push.d 0.5
00194: conv.d.v
00195: push.i 16777215
00197: conv.i.v
00198: push.i 65535
00200: conv.i.v
00201: call.i merge_color(argc=3)
00203: pop.v.v self.COL_PLUS
00205: pushi.e 1
00206: pop.v.i self.BGMADE
00208: pushi.e 0
00209: pop.v.i self.BG_ALPHA
00211: pushi.e 0
00212: pop.v.i self.ANIM_SINER
00214: pushi.e 0
00215: pop.v.i self.ANIM_SINER_B
00217: pushi.e 0
00218: pop.v.i self.TRUE_ANIM_SINER
00220: pushi.e 0
00221: pop.v.i self.MENU_NO
00223: pushi.e 0
00224: pop.v.i self.i
00226: push.v self.i
00228: pushi.e 8
00229: cmp.i.v LT
00230: bf 00245
00231: pushi.e 0
00232: pushi.e -1
00233: push.v self.i
00235: conv.v.i
00236: pop.v.i [array]MENUCOORD
00238: push.v self.i
00240: pushi.e 1
00241: add.i.v
00242: pop.v.v self.i
00244: b 00226
00245: pushi.e 210
00246: pop.v.i self.XL
00248: pushi.e 40
00249: pop.v.i self.YL
00251: pushi.e 5
00252: pop.v.i self.YS
00254: pushi.e 75
00255: pop.v.i self.HEARTX
00257: pushi.e 110
00258: pop.v.i self.HEARTY
00260: pushi.e 75
00261: pop.v.i self.HEARTXCUR
00263: pushi.e 75
00264: pop.v.i self.HEARTYCUR
00266: pushi.e 0
00267: pop.v.i self.MOVENOISE
00269: pushi.e 0
00270: pop.v.i self.SELNOISE
00272: pushi.e 0
00273: pop.v.i self.BACKNOISE
00275: pushi.e 0
00276: pop.v.i self.DEATHNOISE
00278: pushi.e 2
00279: pop.v.i self.ONEBUFFER
00281: pushi.e 0
00282: pop.v.i self.TWOBUFFER
00284: pushi.e 0
00285: pop.v.i self.THREAT
00287: push.s "" ""@24
00289: pop.v.s self.TEMPMESSAGE
00291: pushi.e 0
00292: pop.v.i self.MESSAGETIMER
00294: call.i scr_84_load_ini(argc=0)
00296: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_initializer2").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 1
00001: conv.i.v
00002: call.i audio_group_is_loaded(argc=1)
00004: conv.v.b
00005: bf func_end
00006: pushi.e 1
00007: pop.v.i self.roomchoice
00009: pushi.e 0
00010: pop.v.i self.menu_go
00012: push.s ""filech1_0""@3503
00014: conv.s.v
00015: call.i ossafe_file_exists(argc=1)
00017: conv.v.b
00018: bf 00022
00019: pushi.e 1
00020: pop.v.i self.menu_go
00022: push.s ""filech1_1""@3505
00024: conv.s.v
00025: call.i ossafe_file_exists(argc=1)
00027: conv.v.b
00028: bf 00032
00029: pushi.e 1
00030: pop.v.i self.menu_go
00032: push.s ""filech1_2""@3507
00034: conv.s.v
00035: call.i ossafe_file_exists(argc=1)
00037: conv.v.b
00038: bf 00042
00039: pushi.e 1
00040: pop.v.i self.menu_go
00042: push.s ""filech1_3""@9726
00044: conv.s.v
00045: call.i ossafe_file_exists(argc=1)
00047: conv.v.b
00048: bf 00052
00049: pushi.e 1
00050: pop.v.i self.menu_go
00052: push.s ""dr.ini""@2744
00054: conv.s.v
00055: call.i ossafe_file_exists(argc=1)
00057: conv.v.b
00058: bf 00062
00059: pushi.e 1
00060: pop.v.i self.menu_go
00062: push.s ""filech1_3""@9726
00064: conv.s.v
00065: call.i ossafe_file_exists(argc=1)
00067: conv.v.b
00068: bf 00072
00069: pushi.e 2
00070: pop.v.i self.menu_go
00072: push.s ""filech1_4""@9727
00074: conv.s.v
00075: call.i ossafe_file_exists(argc=1)
00077: conv.v.b
00078: bf 00082
00079: pushi.e 2
00080: pop.v.i self.menu_go
00082: push.s ""filech1_5""@9728
00084: conv.s.v
00085: call.i ossafe_file_exists(argc=1)
00087: conv.v.b
00088: bf 00092
00089: pushi.e 2
00090: pop.v.i self.menu_go
00092: push.v self.menu_go
00094: pushi.e 1
00095: cmp.i.v EQ
00096: bf 00100
00097: pushi.e 139
00098: pop.v.i self.roomchoice
00100: push.v self.menu_go
00102: pushi.e 2
00103: cmp.i.v EQ
00104: bf 00124
00105: push.s ""obj_initializer2_slash_Step_0_gml_22_0""@10054
00107: conv.s.v
00108: call.i scr_84_get_lang_string(argc=1)
00110: call.i scr_windowcaption(argc=1)
00112: popz.v
00113: pushi.e 1
00114: pushi.e -5
00115: pushi.e 10
00116: pop.v.i [array]tempflag
00118: pushi.e 132
00119: pop.v.i self.roomchoice
00121: pushi.e 0
00122: pop.v.i global.plot
00124: push.v self.roomchoice
00126: call.i room_goto(argc=1)
00128: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_savemenu").EventHandlerFor(EventType.Create, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 0
00001: pop.v.i self.cur_jewel
00003: pushi.e 0
00004: pop.v.i self.saved
00006: pushi.e 0
00007: pop.v.i self.coord
00009: pushi.e 0
00010: pop.v.i self.ini_ex
00012: pushi.e 3
00013: pop.v.i self.buffer
00015: push.s ""obj_savemenu_slash_Create_0_gml_7_0""@10034
00017: conv.s.v
00018: call.i scr_84_get_lang_string(argc=1)
00020: pop.v.v self.name
00022: pushi.e 1
00023: pop.v.i self.level
00025: pushi.e 1
00026: pop.v.i self.love
00028: pushi.e 0
00029: pop.v.i self.time
00031: pushi.e 0
00032: pop.v.i self.roome
00034: pushi.e 0
00035: pop.v.i self.endme
00037: pushvar.v self.room
00039: pop.v.v global.currentroom
00041: pushi.e 1
00042: pop.v.i global.interact
00044: push.s ""dr.ini""@2744
00046: conv.s.v
00047: call.i ossafe_file_exists(argc=1)
00049: conv.v.b
00050: bf 00145
00051: pushi.e 1
00052: pop.v.i self.ini_ex
00054: push.s ""dr.ini""@2744
00056: conv.s.v
00057: call.i ossafe_ini_open(argc=1)
00059: pop.v.v self.iniread
00061: push.s ""Kris""@10040
00063: conv.s.v
00064: push.s ""Name""@2747
00066: conv.s.v
00067: push.s ""G""@2534
00069: pushglb.v global.filechoice
00071: call.i string(argc=1)
00073: add.v.s
00074: call.i ini_read_string(argc=3)
00076: pop.v.v self.name
00078: pushi.e 1
00079: conv.i.v
00080: push.s ""Level""@2749
00082: conv.s.v
00083: push.s ""G""@2534
00085: pushglb.v global.filechoice
00087: call.i string(argc=1)
00089: add.v.s
00090: call.i ini_read_real(argc=3)
00092: pop.v.v self.level
00094: pushi.e 1
00095: conv.i.v
00096: push.s ""Love""@2751
00098: conv.s.v
00099: push.s ""G""@2534
00101: pushglb.v global.filechoice
00103: call.i string(argc=1)
00105: add.v.s
00106: call.i ini_read_real(argc=3)
00108: pop.v.v self.love
00110: pushi.e 0
00111: conv.i.v
00112: push.s ""Time""@2752
00114: conv.s.v
00115: push.s ""G""@2534
00117: pushglb.v global.filechoice
00119: call.i string(argc=1)
00121: add.v.s
00122: call.i ini_read_real(argc=3)
00124: pop.v.v self.time
00126: pushi.e 0
00127: conv.i.v
00128: push.s ""Room""@2753
00130: conv.s.v
00131: push.s ""G""@2534
00133: pushglb.v global.filechoice
00135: call.i string(argc=1)
00137: add.v.s
00138: call.i ini_read_real(argc=3)
00140: pop.v.v self.roome
00142: call.i ossafe_ini_close(argc=0)
00144: popz.v
00145: pushglb.v global.darkzone
00147: pushi.e 1
00148: add.i.v
00149: pop.v.v self.d
00151: push.v self.time
00153: pushi.e 1800
00154: conv.i.d
00155: div.d.v
00156: call.i floor(argc=1)
00158: pop.v.v self.minutes
00160: push.v self.time
00162: pushi.e 1800
00163: conv.i.d
00164: div.d.v
00165: push.v self.minutes
00167: sub.v.v
00168: pushi.e 60
00169: mul.i.v
00170: call.i round(argc=1)
00172: pop.v.v self.seconds
00174: push.v self.seconds
00176: pushi.e 60
00177: cmp.i.v EQ
00178: bf 00182
00179: pushi.e 59
00180: pop.v.i self.seconds
00182: push.v self.seconds
00184: pushi.e 10
00185: cmp.i.v LT
00186: bf 00196
00187: push.s ""0""@2521
00189: push.v self.seconds
00191: call.i string(argc=1)
00193: add.v.s
00194: pop.v.v self.seconds
00196: push.v self.roome
00198: call.i scr_roomname(argc=1)
00200: popz.v
00201: push.v self.d
00203: pushi.e 2
00204: cmp.i.v EQ
00205: bf 00209
00206: pushi.e 908
00207: pop.v.i self.heartsprite
00209: push.v self.d
00211: pushi.e 1
00212: cmp.i.v EQ
00213: bf 00217
00214: pushi.e 910
00215: pop.v.i self.heartsprite
00217: push.v self.d
00219: pushi.e 1
00220: cmp.i.v EQ
00221: bf func_end
00222: push.s ""obj_savemenu_slash_Create_0_gml_43_0""@10044
00224: conv.s.v
00225: call.i scr_84_get_lang_string(argc=1)
00227: pop.v.v self.name
", Data.Functions, Data.Variables, Data.Strings));

//gml_Script_scr_84_lang_load
Data.Scripts.ByName("scr_84_lang_load")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 name 1133
.localvar 2 orig_filename 1135
.localvar 3 new_filename 1136
.localvar 4 filename 1137
.localvar 5 type 1138
.localvar 6 orig_map 1139
.localvar 7 new_map 1140
.localvar 8 new_date 1141
.localvar 9 orig_date 1142
00000: push.s ""lang_""@3408
00002: pushglb.v global.lang
00004: add.v.s
00005: push.s "".json""@3409
00007: add.s.v
00008: pop.v.v local.name
00010: pushvar.v self.working_directory
00012: push.s ""lang/""@3411
00014: add.s.v
00015: pushloc.v local.name
00017: add.v.v
00018: pop.v.v local.orig_filename
00020: pushvar.v self.working_directory
00022: push.s ""lang-new/""@3413
00024: add.s.v
00025: pushloc.v local.name
00027: add.v.v
00028: pop.v.v local.new_filename
00030: pushloc.v local.orig_filename
00032: pop.v.v local.filename
00034: push.s ""orig""@3415
00036: pop.v.s local.type
00038: push.s ""loading lang: ""@3416
00040: pushloc.v local.orig_filename
00042: add.v.s
00043: call.i show_debug_message(argc=1)
00045: popz.v
00046: pushloc.v local.orig_filename
00048: call.i scr_84_load_map_json(argc=1)
00050: pop.v.v local.orig_map
00052: pushloc.v local.new_filename
00054: call.i ossafe_file_exists(argc=1)
00056: conv.v.b
00057: bf 00150
00058: pushloc.v local.new_filename
00060: call.i scr_84_load_map_json(argc=1)
00062: pop.v.v local.new_map
00064: push.s ""date""@3421
00066: conv.s.v
00067: pushloc.v local.new_map
00069: call.i ds_map_find_value(argc=2)
00071: call.i real(argc=1)
00073: pop.v.v local.new_date
00075: push.s ""date""@3421
00077: conv.s.v
00078: pushloc.v local.orig_map
00080: call.i ds_map_find_value(argc=2)
00082: call.i real(argc=1)
00084: pop.v.v local.orig_date
00086: push.s ""orig_date: ""@3424
00088: pushloc.v local.orig_date
00090: call.i string(argc=1)
00092: add.v.s
00093: call.i show_debug_message(argc=1)
00095: popz.v
00096: push.s "" new_date: ""@3425
00098: pushloc.v local.new_date
00100: call.i string(argc=1)
00102: add.v.s
00103: call.i show_debug_message(argc=1)
00105: popz.v
00106: pushloc.v local.new_date
00108: pushloc.v local.orig_date
00110: cmp.v.v GT
00111: bf 00144
00112: push.s ""using new language file""@3426
00114: conv.s.v
00115: call.i show_debug_message(argc=1)
00117: popz.v
00118: pushloc.v local.orig_map
00120: call.i ds_map_destroy(argc=1)
00122: popz.v
00123: pushloc.v local.new_map
00125: pop.v.v local.orig_map
00127: pushloc.v local.new_filename
00129: pop.v.v local.filename
00131: push.s ""new(""@3427
00133: pushloc.v local.new_date
00135: call.i string(argc=1)
00137: add.v.s
00138: push.s "")""@3428
00140: add.s.v
00141: pop.v.v local.type
00143: b 00150
00144: push.s ""using orig language file""@3429
00146: conv.s.v
00147: call.i show_debug_message(argc=1)
00149: popz.v
00150: pushglb.v global.lang_map
00152: call.i ds_map_destroy(argc=1)
00154: popz.v
00155: pushloc.v local.orig_map
00157: pop.v.v global.lang_map
00159: push.s ""loaded: ""@3430
00161: pushloc.v local.filename
00163: add.v.s
00164: push.s "", entries: ""@3431
00166: add.s.v
00167: pushglb.v global.lang_map
00169: call.i ds_map_size(argc=1)
00171: call.i string(argc=1)
00173: add.v.v
00174: call.i show_debug_message(argc=1)
00176: popz.v
00177: pushloc.v local.type
00179: ret.v
", Data.Functions, Data.Variables, Data.Strings));

//gml_Script_scr_84_load_ini
Data.Scripts.ByName("scr_84_load_ini")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 0
00001: pop.v.i self.i
00003: push.v self.i
00005: pushi.e 3
00006: cmp.i.v LT
00007: bf 00022
00008: pushi.e 0
00009: pushi.e -1
00010: push.v self.i
00012: conv.v.i
00013: pop.v.i [array]FILE
00015: push.v self.i
00017: pushi.e 1
00018: add.i.v
00019: pop.v.v self.i
00021: b 00003
00022: pushi.e 0
00023: pop.v.i self.i
00025: push.v self.i
00027: pushi.e 3
00028: cmp.i.v LT
00029: bf 00092
00030: pushi.e 0
00031: pushi.e -1
00032: push.v self.i
00034: conv.v.i
00035: pop.v.i [array]FILE
00037: push.s ""DEVICE_MENU_slash_Create_0_gml_97_0""@3495
00039: conv.s.v
00040: call.i scr_84_get_lang_string(argc=1)
00042: pushi.e -1
00043: push.v self.i
00045: conv.v.i
00046: pop.v.v [array]NAME
00048: pushi.e 0
00049: pushi.e -1
00050: push.v self.i
00052: conv.v.i
00053: pop.v.i [array]TIME
00055: push.s ""------------""@3497
00057: pushi.e -1
00058: push.v self.i
00060: conv.v.i
00061: pop.v.s [array]PLACE
00063: pushi.e 0
00064: pushi.e -1
00065: push.v self.i
00067: conv.v.i
00068: pop.v.i [array]LEVEL
00070: push.s ""--:--""@3500
00072: pushi.e -1
00073: push.v self.i
00075: conv.v.i
00076: pop.v.s [array]TIME_STRING
00078: pushi.e 0
00079: pushi.e -1
00080: push.v self.i
00082: conv.v.i
00083: pop.v.i [array]INITLANG
00085: push.v self.i
00087: pushi.e 1
00088: add.i.v
00089: pop.v.v self.i
00091: b 00025
00092: push.s ""filech1_0""@3503
00094: conv.s.v
00095: call.i ossafe_file_exists(argc=1)
00097: conv.v.b
00098: bf 00113
00099: pushi.e 1
00100: pushi.e -1
00101: pushi.e 0
00102: pop.v.i [array]FILE
00104: push.s ""DEVICE_MENU_slash_Create_0_gml_107_0""@3504
00106: conv.s.v
00107: call.i scr_84_get_lang_string(argc=1)
00109: pushi.e -1
00110: pushi.e 0
00111: pop.v.v [array]NAME
00113: push.s ""filech1_1""@3505
00115: conv.s.v
00116: call.i ossafe_file_exists(argc=1)
00118: conv.v.b
00119: bf 00134
00120: pushi.e 1
00121: pushi.e -1
00122: pushi.e 1
00123: pop.v.i [array]FILE
00125: push.s ""DEVICE_MENU_slash_Create_0_gml_112_0""@3506
00127: conv.s.v
00128: call.i scr_84_get_lang_string(argc=1)
00130: pushi.e -1
00131: pushi.e 1
00132: pop.v.v [array]NAME
00134: push.s ""filech1_2""@3507
00136: conv.s.v
00137: call.i ossafe_file_exists(argc=1)
00139: conv.v.b
00140: bf 00155
00141: pushi.e 1
00142: pushi.e -1
00143: pushi.e 2
00144: pop.v.i [array]FILE
00146: push.s ""DEVICE_MENU_slash_Create_0_gml_117_0""@3508
00148: conv.s.v
00149: call.i scr_84_get_lang_string(argc=1)
00151: pushi.e -1
00152: pushi.e 2
00153: pop.v.v [array]NAME
00155: push.s ""dr.ini""@2744
00157: conv.s.v
00158: call.i ossafe_file_exists(argc=1)
00160: conv.v.b
00161: bf func_end
00162: push.s ""dr.ini""@2744
00164: conv.s.v
00165: call.i ossafe_ini_open(argc=1)
00167: popz.v
00168: pushi.e 0
00169: pop.v.i self.i
00171: push.v self.i
00173: pushi.e 3
00174: cmp.i.v LT
00175: bf 00429
00176: pushi.e -1
00177: push.v self.i
00179: conv.v.i
00180: push.v [array]FILE
00182: pushi.e 1
00183: cmp.i.v EQ
00184: bf 00422
00185: pushi.e 0
00186: conv.i.v
00187: push.s ""Room""@2753
00189: conv.s.v
00190: push.s ""G""@2534
00192: push.v self.i
00194: call.i string(argc=1)
00196: add.v.s
00197: call.i ini_read_real(argc=3)
00199: call.i scr_roomname(argc=1)
00201: pushi.e -1
00202: push.v self.i
00204: conv.v.i
00205: pop.v.v [array]PLACE
00207: pushi.e 0
00208: conv.i.v
00209: push.s ""Time""@2752
00211: conv.s.v
00212: push.s ""G""@2534
00214: push.v self.i
00216: call.i string(argc=1)
00218: add.v.s
00219: call.i ini_read_real(argc=3)
00221: pushi.e -1
00222: push.v self.i
00224: conv.v.i
00225: pop.v.v [array]TIME
00227: push.s ""------""@3511
00229: conv.s.v
00230: push.s ""Name""@2747
00232: conv.s.v
00233: push.s ""G""@2534
00235: push.v self.i
00237: call.i string(argc=1)
00239: add.v.s
00240: call.i ini_read_string(argc=3)
00242: pushi.e -1
00243: push.v self.i
00245: conv.v.i
00246: pop.v.v [array]NAME
00248: pushi.e 1
00249: pushi.e -1
00250: push.v self.i
00252: conv.v.i
00253: pop.v.i [array]LEVEL
00255: pushi.e 0
00256: conv.i.v
00257: push.s ""InitLang""@2754
00259: conv.s.v
00260: push.s ""G""@2534
00262: push.v self.i
00264: call.i string(argc=1)
00266: add.v.s
00267: call.i ini_read_real(argc=3)
00269: pushi.e -1
00270: push.v self.i
00272: conv.v.i
00273: pop.v.v [array]INITLANG
00275: pushi.e -1
00276: push.v self.i
00278: conv.v.i
00279: push.v [array]TIME
00281: pushi.e 30
00282: conv.i.d
00283: div.d.v
00284: call.i floor(argc=1)
00286: pushi.e -1
00287: push.v self.i
00289: conv.v.i
00290: pop.v.v [array]TIME_SECONDS_TOTAL
00292: pushi.e -1
00293: push.v self.i
00295: conv.v.i
00296: push.v [array]TIME_SECONDS_TOTAL
00298: pushi.e 60
00299: conv.i.d
00300: div.d.v
00301: call.i floor(argc=1)
00303: pushi.e -1
00304: push.v self.i
00306: conv.v.i
00307: pop.v.v [array]TIME_MINUTES
00309: pushi.e -1
00310: push.v self.i
00312: conv.v.i
00313: push.v [array]TIME_SECONDS_TOTAL
00315: pushi.e -1
00316: push.v self.i
00318: conv.v.i
00319: push.v [array]TIME_MINUTES
00321: pushi.e 60
00322: mul.i.v
00323: sub.v.v
00324: pushi.e -1
00325: push.v self.i
00327: conv.v.i
00328: pop.v.v [array]TIME_SECONDS
00330: pushi.e -1
00331: push.v self.i
00333: conv.v.i
00334: push.v [array]TIME_SECONDS
00336: call.i string(argc=1)
00338: pushi.e -1
00339: push.v self.i
00341: conv.v.i
00342: pop.v.v [array]TIME_SECONDS_STRING
00344: pushi.e -1
00345: push.v self.i
00347: conv.v.i
00348: push.v [array]TIME_SECONDS
00350: pushi.e 0
00351: cmp.i.v EQ
00352: bf 00361
00353: push.s ""00""@3517
00355: pushi.e -1
00356: push.v self.i
00358: conv.v.i
00359: pop.v.s [array]TIME_SECONDS_STRING
00361: pushi.e -1
00362: push.v self.i
00364: conv.v.i
00365: push.v [array]TIME_SECONDS
00367: pushi.e 10
00368: cmp.i.v LT
00369: bf 00379
00370: pushi.e -1
00371: push.v self.i
00373: conv.v.i
00374: push.v [array]TIME_SECONDS
00376: pushi.e 1
00377: cmp.i.v GTE
00378: b 00380
00379: push.e 0
00380: bf 00398
00381: push.s ""0""@2521
00383: pushi.e -1
00384: push.v self.i
00386: conv.v.i
00387: push.v [array]TIME_SECONDS
00389: call.i string(argc=1)
00391: add.v.s
00392: pushi.e -1
00393: push.v self.i
00395: conv.v.i
00396: pop.v.v [array]TIME_SECONDS_STRING
00398: pushi.e -1
00399: push.v self.i
00401: conv.v.i
00402: push.v [array]TIME_MINUTES
00404: call.i string(argc=1)
00406: push.s "":""@1546
00408: add.s.v
00409: pushi.e -1
00410: push.v self.i
00412: conv.v.i
00413: push.v [array]TIME_SECONDS_STRING
00415: add.v.v
00416: pushi.e -1
00417: push.v self.i
00419: conv.v.i
00420: pop.v.v [array]TIME_STRING
00422: push.v self.i
00424: pushi.e 1
00425: add.i.v
00426: pop.v.v self.i
00428: b 00171
00429: call.i ossafe_ini_close(argc=0)
00431: popz.v
", Data.Functions, Data.Variables, Data.Strings));

//gml_Script_scr_84_load_map_json
Data.Scripts.ByName("scr_84_load_map_json")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 filename 1123
.localvar 2 file 1124
.localvar 3 json 1125
00000: pushvar.v self.argument0
00002: pop.v.v local.filename
00004: pushloc.v local.filename
00006: call.i ossafe_file_text_open_read(argc=1)
00008: pop.v.v local.file
00010: push.s """"@2240
00012: pop.v.s local.json
00014: pushloc.v local.file
00016: call.i ossafe_file_text_eof(argc=1)
00018: pushi.e 0
00019: cmp.i.v EQ
00020: bf 00031
00021: push.v local.json
00023: pushloc.v local.file
00025: call.i ossafe_file_text_readln(argc=1)
00027: add.v.v
00028: pop.v.v local.json
00030: b 00014
00031: pushloc.v local.file
00033: call.i ossafe_file_text_close(argc=1)
00035: popz.v
00036: pushloc.v local.json
00038: call.i json_decode(argc=1)
00040: ret.v
", Data.Functions, Data.Variables, Data.Strings));

//gml_Script_scr_load
Data.Scripts.ByName("scr_load")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: call.i snd_free_all(argc=0)
00002: popz.v
00003: pushglb.v global.filechoice
00005: pop.v.v self.filechoicebk
00007: call.i scr_gamestart(argc=0)
00009: popz.v
00010: push.v self.filechoicebk
00012: pop.v.v global.filechoice
00014: push.s ""filech1_""@2713
00016: pushglb.v global.filechoice
00018: call.i string(argc=1)
00020: add.v.s
00021: pop.v.v self.file
00023: push.v self.file
00025: call.i ossafe_file_text_open_read(argc=1)
00027: pop.v.v self.myfileid
00029: push.v self.myfileid
00031: call.i ossafe_file_text_read_string(argc=1)
00033: pop.v.v global.truename
00035: push.v self.myfileid
00037: call.i ossafe_file_text_readln(argc=1)
00039: popz.v
00040: pushi.e 0
00041: pop.v.i self.i
00043: push.v self.i
00045: pushi.e 6
00046: cmp.i.v LT
00047: bf 00070
00048: push.v self.myfileid
00050: call.i ossafe_file_text_read_string(argc=1)
00052: pushi.e -5
00053: push.v self.i
00055: conv.v.i
00056: pop.v.v [array]othername
00058: push.v self.myfileid
00060: call.i ossafe_file_text_readln(argc=1)
00062: popz.v
00063: push.v self.i
00065: pushi.e 1
00066: add.i.v
00067: pop.v.v self.i
00069: b 00043
00070: push.v self.myfileid
00072: call.i ossafe_file_text_read_real(argc=1)
00074: pushi.e -5
00075: pushi.e 0
00076: pop.v.v [array]char
00078: push.v self.myfileid
00080: call.i ossafe_file_text_readln(argc=1)
00082: popz.v
00083: push.v self.myfileid
00085: call.i ossafe_file_text_read_real(argc=1)
00087: pushi.e -5
00088: pushi.e 1
00089: pop.v.v [array]char
00091: push.v self.myfileid
00093: call.i ossafe_file_text_readln(argc=1)
00095: popz.v
00096: push.v self.myfileid
00098: call.i ossafe_file_text_read_real(argc=1)
00100: pushi.e -5
00101: pushi.e 2
00102: pop.v.v [array]char
00104: push.v self.myfileid
00106: call.i ossafe_file_text_readln(argc=1)
00108: popz.v
00109: push.v self.myfileid
00111: call.i ossafe_file_text_read_real(argc=1)
00113: pop.v.v global.gold
00115: push.v self.myfileid
00117: call.i ossafe_file_text_readln(argc=1)
00119: popz.v
00120: push.v self.myfileid
00122: call.i ossafe_file_text_read_real(argc=1)
00124: pop.v.v global.xp
00126: push.v self.myfileid
00128: call.i ossafe_file_text_readln(argc=1)
00130: popz.v
00131: push.v self.myfileid
00133: call.i ossafe_file_text_read_real(argc=1)
00135: pop.v.v global.lv
00137: push.v self.myfileid
00139: call.i ossafe_file_text_readln(argc=1)
00141: popz.v
00142: push.v self.myfileid
00144: call.i ossafe_file_text_read_real(argc=1)
00146: pop.v.v global.inv
00148: push.v self.myfileid
00150: call.i ossafe_file_text_readln(argc=1)
00152: popz.v
00153: push.v self.myfileid
00155: call.i ossafe_file_text_read_real(argc=1)
00157: pop.v.v global.invc
00159: push.v self.myfileid
00161: call.i ossafe_file_text_readln(argc=1)
00163: popz.v
00164: push.v self.myfileid
00166: call.i ossafe_file_text_read_real(argc=1)
00168: pop.v.v global.darkzone
00170: push.v self.myfileid
00172: call.i ossafe_file_text_readln(argc=1)
00174: popz.v
00175: pushi.e 0
00176: pop.v.i self.i
00178: push.v self.i
00180: pushi.e 4
00181: cmp.i.v LT
00182: bf 00586
00183: push.v self.myfileid
00185: call.i ossafe_file_text_read_real(argc=1)
00187: pushi.e -5
00188: push.v self.i
00190: conv.v.i
00191: pop.v.v [array]hp
00193: push.v self.myfileid
00195: call.i ossafe_file_text_readln(argc=1)
00197: popz.v
00198: push.v self.myfileid
00200: call.i ossafe_file_text_read_real(argc=1)
00202: pushi.e -5
00203: push.v self.i
00205: conv.v.i
00206: pop.v.v [array]maxhp
00208: push.v self.myfileid
00210: call.i ossafe_file_text_readln(argc=1)
00212: popz.v
00213: push.v self.myfileid
00215: call.i ossafe_file_text_read_real(argc=1)
00217: pushi.e -5
00218: push.v self.i
00220: conv.v.i
00221: pop.v.v [array]at
00223: push.v self.myfileid
00225: call.i ossafe_file_text_readln(argc=1)
00227: popz.v
00228: push.v self.myfileid
00230: call.i ossafe_file_text_read_real(argc=1)
00232: pushi.e -5
00233: push.v self.i
00235: conv.v.i
00236: pop.v.v [array]df
00238: push.v self.myfileid
00240: call.i ossafe_file_text_readln(argc=1)
00242: popz.v
00243: push.v self.myfileid
00245: call.i ossafe_file_text_read_real(argc=1)
00247: pushi.e -5
00248: push.v self.i
00250: conv.v.i
00251: pop.v.v [array]mag
00253: push.v self.myfileid
00255: call.i ossafe_file_text_readln(argc=1)
00257: popz.v
00258: push.v self.myfileid
00260: call.i ossafe_file_text_read_real(argc=1)
00262: pushi.e -5
00263: push.v self.i
00265: conv.v.i
00266: pop.v.v [array]guts
00268: push.v self.myfileid
00270: call.i ossafe_file_text_readln(argc=1)
00272: popz.v
00273: push.v self.myfileid
00275: call.i ossafe_file_text_read_real(argc=1)
00277: pushi.e -5
00278: push.v self.i
00280: conv.v.i
00281: pop.v.v [array]charweapon
00283: push.v self.myfileid
00285: call.i ossafe_file_text_readln(argc=1)
00287: popz.v
00288: push.v self.myfileid
00290: call.i ossafe_file_text_read_real(argc=1)
00292: pushi.e -5
00293: push.v self.i
00295: conv.v.i
00296: pop.v.v [array]chararmor1
00298: push.v self.myfileid
00300: call.i ossafe_file_text_readln(argc=1)
00302: popz.v
00303: push.v self.myfileid
00305: call.i ossafe_file_text_read_real(argc=1)
00307: pushi.e -5
00308: push.v self.i
00310: conv.v.i
00311: pop.v.v [array]chararmor2
00313: push.v self.myfileid
00315: call.i ossafe_file_text_readln(argc=1)
00317: popz.v
00318: push.v self.myfileid
00320: call.i ossafe_file_text_read_real(argc=1)
00322: pushi.e -5
00323: push.v self.i
00325: conv.v.i
00326: pop.v.v [array]weaponstyle
00328: push.v self.myfileid
00330: call.i ossafe_file_text_readln(argc=1)
00332: popz.v
00333: pushi.e 0
00334: pop.v.i self.q
00336: push.v self.q
00338: pushi.e 4
00339: cmp.i.v LT
00340: bf 00540
00341: push.v self.myfileid
00343: call.i ossafe_file_text_read_real(argc=1)
00345: pushi.e -5
00346: push.v self.i
00348: conv.v.i
00349: break.e -1
00350: push.i 32000
00352: mul.i.i
00353: push.v self.q
00355: conv.v.i
00356: break.e -1
00357: add.i.i
00358: pop.v.v [array]itemat
00360: push.v self.myfileid
00362: call.i ossafe_file_text_readln(argc=1)
00364: popz.v
00365: push.v self.myfileid
00367: call.i ossafe_file_text_read_real(argc=1)
00369: pushi.e -5
00370: push.v self.i
00372: conv.v.i
00373: break.e -1
00374: push.i 32000
00376: mul.i.i
00377: push.v self.q
00379: conv.v.i
00380: break.e -1
00381: add.i.i
00382: pop.v.v [array]itemdf
00384: push.v self.myfileid
00386: call.i ossafe_file_text_readln(argc=1)
00388: popz.v
00389: push.v self.myfileid
00391: call.i ossafe_file_text_read_real(argc=1)
00393: pushi.e -5
00394: push.v self.i
00396: conv.v.i
00397: break.e -1
00398: push.i 32000
00400: mul.i.i
00401: push.v self.q
00403: conv.v.i
00404: break.e -1
00405: add.i.i
00406: pop.v.v [array]itemmag
00408: push.v self.myfileid
00410: call.i ossafe_file_text_readln(argc=1)
00412: popz.v
00413: push.v self.myfileid
00415: call.i ossafe_file_text_read_real(argc=1)
00417: pushi.e -5
00418: push.v self.i
00420: conv.v.i
00421: break.e -1
00422: push.i 32000
00424: mul.i.i
00425: push.v self.q
00427: conv.v.i
00428: break.e -1
00429: add.i.i
00430: pop.v.v [array]itembolts
00432: push.v self.myfileid
00434: call.i ossafe_file_text_readln(argc=1)
00436: popz.v
00437: push.v self.myfileid
00439: call.i ossafe_file_text_read_real(argc=1)
00441: pushi.e -5
00442: push.v self.i
00444: conv.v.i
00445: break.e -1
00446: push.i 32000
00448: mul.i.i
00449: push.v self.q
00451: conv.v.i
00452: break.e -1
00453: add.i.i
00454: pop.v.v [array]itemgrazeamt
00456: push.v self.myfileid
00458: call.i ossafe_file_text_readln(argc=1)
00460: popz.v
00461: push.v self.myfileid
00463: call.i ossafe_file_text_read_real(argc=1)
00465: pushi.e -5
00466: push.v self.i
00468: conv.v.i
00469: break.e -1
00470: push.i 32000
00472: mul.i.i
00473: push.v self.q
00475: conv.v.i
00476: break.e -1
00477: add.i.i
00478: pop.v.v [array]itemgrazesize
00480: push.v self.myfileid
00482: call.i ossafe_file_text_readln(argc=1)
00484: popz.v
00485: push.v self.myfileid
00487: call.i ossafe_file_text_read_real(argc=1)
00489: pushi.e -5
00490: push.v self.i
00492: conv.v.i
00493: break.e -1
00494: push.i 32000
00496: mul.i.i
00497: push.v self.q
00499: conv.v.i
00500: break.e -1
00501: add.i.i
00502: pop.v.v [array]itemboltspeed
00504: push.v self.myfileid
00506: call.i ossafe_file_text_readln(argc=1)
00508: popz.v
00509: push.v self.myfileid
00511: call.i ossafe_file_text_read_real(argc=1)
00513: pushi.e -5
00514: push.v self.i
00516: conv.v.i
00517: break.e -1
00518: push.i 32000
00520: mul.i.i
00521: push.v self.q
00523: conv.v.i
00524: break.e -1
00525: add.i.i
00526: pop.v.v [array]itemspecial
00528: push.v self.myfileid
00530: call.i ossafe_file_text_readln(argc=1)
00532: popz.v
00533: push.v self.q
00535: pushi.e 1
00536: add.i.v
00537: pop.v.v self.q
00539: b 00336
00540: pushi.e 0
00541: pop.v.i self.j
00543: push.v self.j
00545: pushi.e 12
00546: cmp.i.v LT
00547: bf 00579
00548: push.v self.myfileid
00550: call.i ossafe_file_text_read_real(argc=1)
00552: pushi.e -5
00553: push.v self.i
00555: conv.v.i
00556: break.e -1
00557: push.i 32000
00559: mul.i.i
00560: push.v self.j
00562: conv.v.i
00563: break.e -1
00564: add.i.i
00565: pop.v.v [array]spell
00567: push.v self.myfileid
00569: call.i ossafe_file_text_readln(argc=1)
00571: popz.v
00572: push.v self.j
00574: pushi.e 1
00575: add.i.v
00576: pop.v.v self.j
00578: b 00543
00579: push.v self.i
00581: pushi.e 1
00582: add.i.v
00583: pop.v.v self.i
00585: b 00178
00586: push.v self.myfileid
00588: call.i ossafe_file_text_read_real(argc=1)
00590: pop.v.v global.boltspeed
00592: push.v self.myfileid
00594: call.i ossafe_file_text_readln(argc=1)
00596: popz.v
00597: push.v self.myfileid
00599: call.i ossafe_file_text_read_real(argc=1)
00601: pop.v.v global.grazeamt
00603: push.v self.myfileid
00605: call.i ossafe_file_text_readln(argc=1)
00607: popz.v
00608: push.v self.myfileid
00610: call.i ossafe_file_text_read_real(argc=1)
00612: pop.v.v global.grazesize
00614: push.v self.myfileid
00616: call.i ossafe_file_text_readln(argc=1)
00618: popz.v
00619: pushi.e 0
00620: pop.v.i self.j
00622: push.v self.j
00624: pushi.e 13
00625: cmp.i.v LT
00626: bf 00694
00627: push.v self.myfileid
00629: call.i ossafe_file_text_read_real(argc=1)
00631: pushi.e -5
00632: push.v self.j
00634: conv.v.i
00635: pop.v.v [array]item
00637: push.v self.myfileid
00639: call.i ossafe_file_text_readln(argc=1)
00641: popz.v
00642: push.v self.myfileid
00644: call.i ossafe_file_text_read_real(argc=1)
00646: pushi.e -5
00647: push.v self.j
00649: conv.v.i
00650: pop.v.v [array]keyitem
00652: push.v self.myfileid
00654: call.i ossafe_file_text_readln(argc=1)
00656: popz.v
00657: push.v self.myfileid
00659: call.i ossafe_file_text_read_real(argc=1)
00661: pushi.e -5
00662: push.v self.j
00664: conv.v.i
00665: pop.v.v [array]weapon
00667: push.v self.myfileid
00669: call.i ossafe_file_text_readln(argc=1)
00671: popz.v
00672: push.v self.myfileid
00674: call.i ossafe_file_text_read_real(argc=1)
00676: pushi.e -5
00677: push.v self.j
00679: conv.v.i
00680: pop.v.v [array]armor
00682: push.v self.myfileid
00684: call.i ossafe_file_text_readln(argc=1)
00686: popz.v
00687: push.v self.j
00689: pushi.e 1
00690: add.i.v
00691: pop.v.v self.j
00693: b 00622
00694: push.v self.myfileid
00696: call.i ossafe_file_text_read_real(argc=1)
00698: pop.v.v global.tension
00700: push.v self.myfileid
00702: call.i ossafe_file_text_readln(argc=1)
00704: popz.v
00705: push.v self.myfileid
00707: call.i ossafe_file_text_read_real(argc=1)
00709: pop.v.v global.maxtension
00711: push.v self.myfileid
00713: call.i ossafe_file_text_readln(argc=1)
00715: popz.v
00716: push.v self.myfileid
00718: call.i ossafe_file_text_read_real(argc=1)
00720: pop.v.v global.lweapon
00722: push.v self.myfileid
00724: call.i ossafe_file_text_readln(argc=1)
00726: popz.v
00727: push.v self.myfileid
00729: call.i ossafe_file_text_read_real(argc=1)
00731: pop.v.v global.larmor
00733: push.v self.myfileid
00735: call.i ossafe_file_text_readln(argc=1)
00737: popz.v
00738: push.v self.myfileid
00740: call.i ossafe_file_text_read_real(argc=1)
00742: pop.v.v global.lxp
00744: push.v self.myfileid
00746: call.i ossafe_file_text_readln(argc=1)
00748: popz.v
00749: push.v self.myfileid
00751: call.i ossafe_file_text_read_real(argc=1)
00753: pop.v.v global.llv
00755: push.v self.myfileid
00757: call.i ossafe_file_text_readln(argc=1)
00759: popz.v
00760: push.v self.myfileid
00762: call.i ossafe_file_text_read_real(argc=1)
00764: pop.v.v global.lgold
00766: push.v self.myfileid
00768: call.i ossafe_file_text_readln(argc=1)
00770: popz.v
00771: push.v self.myfileid
00773: call.i ossafe_file_text_read_real(argc=1)
00775: pop.v.v global.lhp
00777: push.v self.myfileid
00779: call.i ossafe_file_text_readln(argc=1)
00781: popz.v
00782: push.v self.myfileid
00784: call.i ossafe_file_text_read_real(argc=1)
00786: pop.v.v global.lmaxhp
00788: push.v self.myfileid
00790: call.i ossafe_file_text_readln(argc=1)
00792: popz.v
00793: push.v self.myfileid
00795: call.i ossafe_file_text_read_real(argc=1)
00797: pop.v.v global.lat
00799: push.v self.myfileid
00801: call.i ossafe_file_text_readln(argc=1)
00803: popz.v
00804: push.v self.myfileid
00806: call.i ossafe_file_text_read_real(argc=1)
00808: pop.v.v global.ldf
00810: push.v self.myfileid
00812: call.i ossafe_file_text_readln(argc=1)
00814: popz.v
00815: push.v self.myfileid
00817: call.i ossafe_file_text_read_real(argc=1)
00819: pop.v.v global.lwstrength
00821: push.v self.myfileid
00823: call.i ossafe_file_text_readln(argc=1)
00825: popz.v
00826: push.v self.myfileid
00828: call.i ossafe_file_text_read_real(argc=1)
00830: pop.v.v global.ladef
00832: push.v self.myfileid
00834: call.i ossafe_file_text_readln(argc=1)
00836: popz.v
00837: pushi.e 0
00838: pop.v.i self.i
00840: push.v self.i
00842: pushi.e 8
00843: cmp.i.v LT
00844: bf 00882
00845: push.v self.myfileid
00847: call.i ossafe_file_text_read_real(argc=1)
00849: pushi.e -5
00850: push.v self.i
00852: conv.v.i
00853: pop.v.v [array]litem
00855: push.v self.myfileid
00857: call.i ossafe_file_text_readln(argc=1)
00859: popz.v
00860: push.v self.myfileid
00862: call.i ossafe_file_text_read_real(argc=1)
00864: pushi.e -5
00865: push.v self.i
00867: conv.v.i
00868: pop.v.v [array]phone
00870: push.v self.myfileid
00872: call.i ossafe_file_text_readln(argc=1)
00874: popz.v
00875: push.v self.i
00877: pushi.e 1
00878: add.i.v
00879: pop.v.v self.i
00881: b 00840
00882: pushi.e 0
00883: pop.v.i self.i
00885: push.v self.i
00887: pushi.e 9999
00888: cmp.i.v LT
00889: bf 00912
00890: push.v self.myfileid
00892: call.i ossafe_file_text_read_real(argc=1)
00894: pushi.e -5
00895: push.v self.i
00897: conv.v.i
00898: pop.v.v [array]flag
00900: push.v self.myfileid
00902: call.i ossafe_file_text_readln(argc=1)
00904: popz.v
00905: push.v self.i
00907: pushi.e 1
00908: add.i.v
00909: pop.v.v self.i
00911: b 00885
00912: push.v self.myfileid
00914: call.i ossafe_file_text_read_real(argc=1)
00916: pop.v.v global.plot
00918: push.v self.myfileid
00920: call.i ossafe_file_text_readln(argc=1)
00922: popz.v
00923: push.v self.myfileid
00925: call.i ossafe_file_text_read_real(argc=1)
00927: pop.v.v global.currentroom
00929: push.v self.myfileid
00931: call.i ossafe_file_text_readln(argc=1)
00933: popz.v
00934: push.v self.myfileid
00936: call.i ossafe_file_text_read_real(argc=1)
00938: pop.v.v global.time
00940: push.v self.myfileid
00942: call.i ossafe_file_text_readln(argc=1)
00944: popz.v
00945: push.v self.myfileid
00947: call.i ossafe_file_text_close(argc=1)
00949: popz.v
00950: pushglb.v global.time
00952: pop.v.v global.lastsavedtime
00954: pushglb.v global.lv
00956: pop.v.v global.lastsavedlv
00958: push.v self.myfileid
00960: call.i ossafe_file_text_close(argc=1)
00962: popz.v
00963: call.i scr_tempsave(argc=0)
00965: popz.v
00966: pushi.e 0
00967: conv.i.v
00968: pushi.e -5
00969: pushi.e 15
00970: push.v [array]flag
00972: pushi.e 1
00973: conv.i.v
00974: call.i audio_group_set_gain(argc=3)
00976: popz.v
00977: pushi.e -5
00978: pushi.e 17
00979: push.v [array]flag
00981: pushi.e 0
00982: conv.i.v
00983: call.i audio_set_master_gain(argc=2)
00985: popz.v
00986: pushglb.v global.plot
00988: pushi.e 156
00989: cmp.i.v GTE
00990: bf 01013
00991: pushi.e 0
00992: pop.v.i self.i
00994: push.v self.i
00996: pushi.e 4
00997: cmp.i.v LT
00998: bf 01013
00999: pushi.e 0
01000: pushi.e -5
01001: push.v self.i
01003: conv.v.i
01004: pop.v.i [array]charauto
01006: push.v self.i
01008: pushi.e 1
01009: add.i.v
01010: pop.v.v self.i
01012: b 00994
01013: pushglb.v global.currentroom
01015: pop.v.v self.__loadedroom
01017: call.i scr_dogcheck(argc=0)
01019: conv.v.b
01020: bf 01024
01021: pushi.e 131
01022: pop.v.i self.__loadedroom
01024: push.v self.__loadedroom
01026: call.i room_goto(argc=1)
01028: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("DEVICE_FAILURE").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.v self.EVENT
00002: pushi.e 1
00003: cmp.i.v EQ
00004: bf 00068
00005: call.i snd_free_all(argc=0)
00007: popz.v
00008: push.s ""AUDIO_DRONE.ogg""@9541
00010: conv.s.v
00011: call.i snd_init(argc=1)
00013: pushi.e -5
00014: pushi.e 0
00015: pop.v.v [array]currentsong
00017: pushi.e -5
00018: pushi.e 0
00019: push.v [array]currentsong
00021: call.i mus_loop(argc=1)
00023: pushi.e -5
00024: pushi.e 1
00025: pop.v.v [array]currentsong
00027: pushi.e 667
00028: pop.v.i global.typer
00030: pushi.e 0
00031: pop.v.i global.fc
00033: push.s ""DEVICE_FAILURE_slash_Step_0_gml_10_0""@9701
00035: conv.s.v
00036: call.i scr_84_get_lang_string(argc=1)
00038: pushi.e -5
00039: pushi.e 0
00040: pop.v.v [array]msg
00042: pushi.e 2
00043: pop.v.i self.EVENT
00045: pushi.e 6
00046: conv.i.v
00047: pushi.e 80
00048: conv.i.v
00049: pushi.e 70
00050: conv.i.v
00051: call.i instance_create(argc=3)
00053: pop.v.v self.W
00055: pushi.e -5
00056: pushi.e 3
00057: push.v [array]tempflag
00059: pushi.e 1
00060: cmp.i.v GTE
00061: bf 00068
00062: pushi.e 6
00063: pushenv 00067
00064: call.i instance_destroy(argc=0)
00066: popz.v
00067: popenv 00064
00068: push.v self.EVENT
00070: pushi.e 0
00071: cmp.i.v EQ
00072: bf 00076
00073: pushi.e 1
00074: pop.v.i self.EVENT
00076: push.v self.EVENT
00078: pushi.e 2
00079: cmp.i.v EQ
00080: bf 00088
00081: pushi.e 6
00082: conv.i.v
00083: call.i instance_exists(argc=1)
00085: conv.v.b
00086: not.b
00087: b 00089
00088: push.e 0
00089: bf 00162
00090: pushi.e 0
00091: pop.v.i self.JA_XOFF
00093: pushglb.v global.lang
00095: push.s ""ja""@1566
00097: cmp.s.v EQ
00098: bf 00102
00099: pushi.e 44
00100: pop.v.i self.JA_XOFF
00102: push.s ""DEVICE_FAILURE_slash_Step_0_gml_28_0""@9702
00104: conv.s.v
00105: call.i scr_84_get_lang_string(argc=1)
00107: pushi.e -5
00108: pushi.e 0
00109: pop.v.v [array]msg
00111: pushi.e -5
00112: pushi.e 3
00113: push.v [array]tempflag
00115: pushi.e 1
00116: cmp.i.v GTE
00117: bf 00130
00118: pushi.e 0
00119: pop.v.i self.JA_XOFF
00121: push.s ""DEVICE_FAILURE_slash_Step_0_gml_32_0""@9703
00123: conv.s.v
00124: call.i scr_84_get_lang_string(argc=1)
00126: pushi.e -5
00127: pushi.e 0
00128: pop.v.v [array]msg
00130: pushi.e 3
00131: pop.v.i self.EVENT
00133: pushi.e 30
00134: pushi.e -1
00135: pushi.e 4
00136: pop.v.i [array]alarm
00138: pushi.e -5
00139: pushi.e 3
00140: push.v [array]tempflag
00142: pushi.e 1
00143: cmp.i.v GTE
00144: bf 00150
00145: pushi.e 15
00146: pushi.e -1
00147: pushi.e 4
00148: pop.v.i [array]alarm
00150: pushi.e 6
00151: conv.i.v
00152: pushi.e 80
00153: conv.i.v
00154: pushi.e 40
00155: push.v self.JA_XOFF
00157: add.v.i
00158: call.i instance_create(argc=3)
00160: pop.v.v self.W
00162: push.v self.EVENT
00164: pushi.e 4
00165: cmp.i.v EQ
00166: bf 00180
00167: pushi.e 309
00168: conv.i.v
00169: pushi.e 120
00170: conv.i.v
00171: pushi.e 100
00172: conv.i.v
00173: call.i instance_create(argc=3)
00175: pop.v.v self.choice
00177: pushi.e 5
00178: pop.v.i self.EVENT
00180: push.v self.EVENT
00182: pushi.e 5
00183: cmp.i.v EQ
00184: bf 00213
00185: pushglb.v global.choice
00187: pushi.e 0
00188: cmp.i.v EQ
00189: bf 00199
00190: pushi.e 6
00191: pushenv 00195
00192: call.i instance_destroy(argc=0)
00194: popz.v
00195: popenv 00192
00196: pushi.e 6
00197: pop.v.i self.EVENT
00199: pushglb.v global.choice
00201: pushi.e 1
00202: cmp.i.v EQ
00203: bf 00213
00204: pushi.e 6
00205: pushenv 00209
00206: call.i instance_destroy(argc=0)
00208: popz.v
00209: popenv 00206
00210: pushi.e 26
00211: pop.v.i self.EVENT
00213: push.v self.EVENT
00215: pushi.e 6
00216: cmp.i.v EQ
00217: bf 00271
00218: call.i snd_free_all(argc=0)
00220: popz.v
00221: pushi.e 1
00222: pushi.e -5
00223: pushi.e 6
00224: pop.v.i [array]flag
00226: push.s ""DEVICE_FAILURE_slash_Step_0_gml_68_0""@9704
00228: conv.s.v
00229: call.i scr_84_get_lang_string(argc=1)
00231: pushi.e -5
00232: pushi.e 0
00233: pop.v.v [array]msg
00235: pushi.e 6
00236: conv.i.v
00237: pushi.e 80
00238: conv.i.v
00239: pushi.e 50
00240: conv.i.v
00241: call.i instance_create(argc=3)
00243: pop.v.v self.W
00245: pushi.e 7
00246: pop.v.i self.EVENT
00248: pushi.e 30
00249: pushi.e -1
00250: pushi.e 4
00251: pop.v.i [array]alarm
00253: pushi.e -5
00254: pushi.e 3
00255: push.v [array]tempflag
00257: pushi.e 1
00258: cmp.i.v GTE
00259: bf 00271
00260: pushi.e 6
00261: pushenv 00265
00262: call.i instance_destroy(argc=0)
00264: popz.v
00265: popenv 00262
00266: pushi.e 1
00267: pushi.e -1
00268: pushi.e 4
00269: pop.v.i [array]alarm
00271: push.v self.EVENT
00273: pushi.e 8
00274: cmp.i.v EQ
00275: bf 00318
00276: pushi.e 1
00277: pop.v.i self.WHITEFADE
00279: push.d 0.01
00282: pop.v.d self.FADEUP
00284: pushi.e 9
00285: pop.v.i self.EVENT
00287: pushi.e 120
00288: pushi.e -1
00289: pushi.e 4
00290: pop.v.i [array]alarm
00292: pushi.e -5
00293: pushi.e 3
00294: push.v [array]tempflag
00296: pushi.e 1
00297: cmp.i.v GTE
00298: bf 00309
00299: push.d 0.03
00302: pop.v.d self.FADEUP
00304: pushi.e 45
00305: pushi.e -1
00306: pushi.e 4
00307: pop.v.i [array]alarm
00309: pushi.e -5
00310: pushi.e 3
00311: dup.i 1
00312: push.v [array]tempflag
00314: pushi.e 1
00315: add.i.v
00316: pop.i.v [array]tempflag
00318: push.v self.EVENT
00320: pushi.e 10
00321: cmp.i.v EQ
00322: bf 00337
00323: push.s ""DEVICE_FAILURE_slash_Step_0_gml_95_0""@9705
00325: conv.s.v
00326: call.i scr_84_get_lang_string(argc=1)
00328: call.i scr_windowcaption(argc=1)
00330: popz.v
00331: call.i scr_tempload(argc=0)
00333: popz.v
00334: pushi.e 11
00335: pop.v.i self.EVENT
00337: push.v self.EVENT
00339: pushi.e 26
00340: cmp.i.v EQ
00341: bf 00367
00342: call.i snd_free_all(argc=0)
00344: popz.v
00345: push.s ""DEVICE_FAILURE_slash_Step_0_gml_103_0""@9706
00347: conv.s.v
00348: call.i scr_84_get_lang_string(argc=1)
00350: pushi.e -5
00351: pushi.e 0
00352: pop.v.v [array]msg
00354: pushi.e 27
00355: pop.v.i self.EVENT
00357: pushi.e 6
00358: conv.i.v
00359: pushi.e 80
00360: conv.i.v
00361: pushi.e 60
00362: conv.i.v
00363: call.i instance_create(argc=3)
00365: pop.v.v self.W
00367: push.v self.EVENT
00369: pushi.e 27
00370: cmp.i.v EQ
00371: bf 00379
00372: pushi.e 6
00373: conv.i.v
00374: call.i instance_exists(argc=1)
00376: conv.v.b
00377: not.b
00378: b 00380
00379: push.e 0
00380: bf 00406
00381: push.s ""AUDIO_DARKNESS.ogg""@9707
00383: conv.s.v
00384: call.i snd_init(argc=1)
00386: pushi.e -5
00387: pushi.e 0
00388: pop.v.v [array]currentsong
00390: pushi.e -5
00391: pushi.e 0
00392: push.v [array]currentsong
00394: call.i mus_play(argc=1)
00396: pushi.e -5
00397: pushi.e 1
00398: pop.v.v [array]currentsong
00400: pushi.e 28
00401: pop.v.i self.EVENT
00403: pushi.e 0
00404: pop.v.i self.DARK_WAIT
00406: push.v self.EVENT
00408: pushi.e 28
00409: cmp.i.v EQ
00410: bf 00437
00411: push.v self.DARK_WAIT
00413: pushi.e 1
00414: add.i.v
00415: pop.v.v self.DARK_WAIT
00417: push.v self.DARK_WAIT
00419: pushi.e 2040
00420: cmp.i.v GTE
00421: bf 00425
00422: call.i ossafe_game_end(argc=0)
00424: popz.v
00425: pushi.e -5
00426: pushi.e 1
00427: push.v [array]currentsong
00429: call.i snd_is_playing(argc=1)
00431: conv.v.b
00432: not.b
00433: bf 00437
00434: call.i ossafe_game_end(argc=0)
00436: popz.v
00437: push.v self.EVENT
00439: pushi.e 0
00440: cmp.i.v GTE
00441: bf 00447
00442: push.v self.EVENT
00444: pushi.e 4
00445: cmp.i.v LTE
00446: b 00448
00447: push.e 0
00448: bf func_end
00449: call.i button2_h(argc=0)
00451: conv.v.b
00452: bf func_end
00453: pushi.e 6
00454: pushenv 00492
00455: push.v self.pos
00457: push.v self.length
00459: pushi.e 3
00460: sub.i.v
00461: cmp.v.v LT
00462: bf 00469
00463: push.v self.pos
00465: pushi.e 2
00466: add.i.v
00467: pop.v.v self.pos
00469: push.v self.specfade
00471: push.d 0.9
00474: cmp.d.v LTE
00475: bf 00484
00476: push.v self.specfade
00478: push.d 0.1
00481: sub.d.v
00482: pop.v.v self.specfade
00484: push.v self.rate
00486: pushi.e 1
00487: cmp.i.v LTE
00488: bf 00492
00489: pushi.e 1
00490: pop.v.i self.rate
00492: popenv 00455
", Data.Functions, Data.Variables, Data.Strings));

//All of this just to replace two calls, are you serious? Insane
Data.GameObjects.ByName("DEVICE_CONTACT").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.v self.EVENT
00002: pushi.e 0
00003: cmp.i.v EQ
00004: bf 00077
00005: pushi.e 666
00006: pop.v.i global.typer
00008: pushi.e 0
00009: pop.v.i global.fc
00011: push.s ""DEVICE_CONTACT_slash_Step_0_gml_5_0""@9544
00013: conv.s.v
00014: call.i scr_84_get_lang_string(argc=1)
00016: pushi.e -5
00017: pushi.e 0
00018: pop.v.v [array]msg
00020: push.s ""DEVICE_CONTACT_slash_Step_0_gml_6_0""@9545
00022: conv.s.v
00023: call.i scr_84_get_lang_string(argc=1)
00025: pushi.e -5
00026: pushi.e 1
00027: pop.v.v [array]msg
00029: push.s ""DEVICE_CONTACT_slash_Step_0_gml_7_0""@9546
00031: conv.s.v
00032: call.i scr_84_get_lang_string(argc=1)
00034: pushi.e -5
00035: pushi.e 2
00036: pop.v.v [array]msg
00038: push.s ""DEVICE_CONTACT_slash_Step_0_gml_8_0""@9547
00040: conv.s.v
00041: call.i scr_84_get_lang_string(argc=1)
00043: pushi.e -5
00044: pushi.e 3
00045: pop.v.v [array]msg
00047: pushi.e 1
00048: pop.v.i self.EVENT
00050: pushglb.v global.lang
00052: push.s ""ja""@1566
00054: cmp.s.v EQ
00055: bf 00067
00056: pushi.e 6
00057: conv.i.v
00058: pushi.e 80
00059: conv.i.v
00060: pushi.e 100
00061: conv.i.v
00062: call.i instance_create(argc=3)
00064: pop.v.v self.W
00066: b 00077
00067: pushi.e 6
00068: conv.i.v
00069: pushi.e 80
00070: conv.i.v
00071: pushi.e 110
00072: conv.i.v
00073: call.i instance_create(argc=3)
00075: pop.v.v self.W
00077: push.v self.EVENT
00079: pushi.e 1
00080: cmp.i.v EQ
00081: bf 00089
00082: pushi.e 6
00083: conv.i.v
00084: call.i instance_exists(argc=1)
00086: conv.v.b
00087: not.b
00088: b 00090
00089: push.e 0
00090: bf 00122
00091: pushi.e 144
00092: conv.i.v
00093: call.i snd_play(argc=1)
00095: popz.v
00096: pushi.e 308
00097: conv.i.v
00098: pushi.e 120
00099: conv.i.v
00100: pushi.e 150
00101: conv.i.v
00102: call.i instance_create(argc=3)
00104: pop.v.v self.SOUL
00106: push.d 0.5
00109: push.v self.SOUL
00111: conv.v.i
00112: pop.v.d [stacktop]momentum
00114: pushi.e 2
00115: pop.v.i self.EVENT
00117: pushi.e 20
00118: pushi.e -1
00119: pushi.e 4
00120: pop.v.i [array]alarm
00122: push.v self.EVENT
00124: pushi.e 3
00125: cmp.i.v EQ
00126: bf 00150
00127: pushi.e 1
00128: pop.v.i self.HEARTMADE
00130: pushi.e 0
00131: pop.v.i self.HSINER
00133: pushi.e 4
00134: pop.v.i self.EVENT
00136: pushi.e 90
00137: pushi.e -1
00138: pushi.e 4
00139: pop.v.i [array]alarm
00141: call.i button2_h(argc=0)
00143: conv.v.b
00144: bf 00150
00145: pushi.e 30
00146: pushi.e -1
00147: pushi.e 4
00148: pop.v.i [array]alarm
00150: push.v self.EVENT
00152: pushi.e 5
00153: cmp.i.v EQ
00154: bf 00206
00155: push.s ""DEVICE_CONTACT_slash_Step_0_gml_33_0""@9549
00157: conv.s.v
00158: call.i scr_84_get_lang_string(argc=1)
00160: pushi.e -5
00161: pushi.e 0
00162: pop.v.v [array]msg
00164: push.s ""DEVICE_CONTACT_slash_Step_0_gml_34_0""@9550
00166: conv.s.v
00167: call.i scr_84_get_lang_string(argc=1)
00169: pushi.e -5
00170: pushi.e 1
00171: pop.v.v [array]msg
00173: push.s ""DEVICE_CONTACT_slash_Step_0_gml_35_0""@9551
00175: conv.s.v
00176: call.i scr_84_get_lang_string(argc=1)
00178: pushi.e -5
00179: pushi.e 2
00180: pop.v.v [array]msg
00182: push.s ""DEVICE_CONTACT_slash_Step_0_gml_36_0""@9552
00184: conv.s.v
00185: call.i scr_84_get_lang_string(argc=1)
00187: pushi.e -5
00188: pushi.e 3
00189: pop.v.v [array]msg
00191: pushi.e 6
00192: conv.i.v
00193: pushi.e 50
00194: conv.i.v
00195: pushi.e 110
00196: conv.i.v
00197: call.i instance_create(argc=3)
00199: pop.v.v self.W
00201: push.d 5.1
00204: pop.v.d self.EVENT
00206: push.v self.EVENT
00208: push.d 5.1
00211: cmp.d.v EQ
00212: bf 00224
00213: pushi.e 6
00214: conv.i.v
00215: call.i instance_exists(argc=1)
00217: conv.v.b
00218: bf 00224
00219: push.v self.FADED
00221: pushi.e 0
00222: cmp.i.v EQ
00223: b 00225
00224: push.e 0
00225: bf 00247
00226: pushi.e -5
00227: pushi.e 20
00228: push.v [array]flag
00230: pushi.e 2
00231: cmp.i.v EQ
00232: bf 00247
00233: pushi.e 2
00234: conv.i.v
00235: pushi.e 0
00236: conv.i.v
00237: pushi.e -5
00238: pushi.e 0
00239: push.v [array]currentsong
00241: call.i mus_volume(argc=3)
00243: popz.v
00244: pushi.e 1
00245: pop.v.i self.FADED
00247: push.v self.EVENT
00249: pushi.e 7
00250: cmp.i.v EQ
00251: bf 00287
00252: push.s ""AUDIO_ANOTHERHIM.ogg""@9553
00254: conv.s.v
00255: call.i snd_init(argc=1)
00257: pushi.e -5
00258: pushi.e 0
00259: pop.v.v [array]currentsong
00261: push.d 0.02
00264: conv.d.v
00265: pushi.e -5
00266: pushi.e 0
00267: push.v [array]currentsong
00269: call.i snd_pitch(argc=2)
00271: popz.v
00272: push.d 0.02
00275: pop.v.d self.PITCH
00277: pushi.e -5
00278: pushi.e 0
00279: push.v [array]currentsong
00281: call.i mus_loop(argc=1)
00283: popz.v
00284: pushi.e 8
00285: pop.v.i self.EVENT
00287: push.v self.EVENT
00289: push.d 6.2
00292: cmp.d.v EQ
00293: bf 00301
00294: pushi.e 6
00295: conv.i.v
00296: call.i instance_exists(argc=1)
00298: conv.v.b
00299: not.b
00300: b 00302
00301: push.e 0
00302: bf 00312
00303: call.i snd_free_all(argc=0)
00305: popz.v
00306: pushi.e 1
00307: pop.v.i self.OBMADE
00309: pushi.e 7
00310: pop.v.i self.EVENT
00312: push.v self.EVENT
00314: push.d 5.1
00317: cmp.d.v EQ
00318: bf 00326
00319: pushi.e 6
00320: conv.i.v
00321: call.i instance_exists(argc=1)
00323: conv.v.b
00324: not.b
00325: b 00327
00326: push.e 0
00327: bf 00367
00328: call.i snd_free_all(argc=0)
00330: popz.v
00331: pushi.e 144
00332: conv.i.v
00333: call.i snd_play(argc=1)
00335: popz.v
00336: pushi.e 0
00337: pop.v.i self.HEARTMADE
00339: push.v self.SOUL
00341: conv.v.i
00342: dup.i 0
00343: push.v [stacktop]t
00345: pushi.e 2
00346: sub.i.v
00347: pop.i.v [stacktop]t
00349: push.d -0.5
00352: push.v self.SOUL
00354: conv.v.i
00355: pop.v.d [stacktop]momentum
00357: push.d 5.2
00360: pop.v.d self.EVENT
00362: pushi.e 60
00363: pushi.e -1
00364: pushi.e 4
00365: pop.v.i [array]alarm
00367: push.v self.EVENT
00369: pushi.e 8
00370: cmp.i.v EQ
00371: bf 00405
00372: push.v self.PITCH
00374: push.d 0.96
00377: cmp.d.v LT
00378: bf 00388
00379: push.v self.PITCH
00381: push.d 0.02
00384: add.d.v
00385: pop.v.v self.PITCH
00387: b 00396
00388: pushi.e 9
00389: pop.v.i self.EVENT
00391: pushi.e 30
00392: pushi.e -1
00393: pushi.e 4
00394: pop.v.i [array]alarm
00396: push.v self.PITCH
00398: pushi.e -5
00399: pushi.e 0
00400: push.v [array]currentsong
00402: call.i snd_pitch(argc=2)
00404: popz.v
00405: push.v self.EVENT
00407: pushi.e 10
00408: cmp.i.v EQ
00409: bf 00444
00410: pushi.e 667
00411: pop.v.i global.typer
00413: push.s ""DEVICE_CONTACT_slash_Step_0_gml_107_0""@9555
00415: conv.s.v
00416: call.i scr_84_get_lang_string(argc=1)
00418: pushi.e -5
00419: pushi.e 0
00420: pop.v.v [array]msg
00422: push.s ""DEVICE_CONTACT_slash_Step_0_gml_111_0""@9556
00424: conv.s.v
00425: call.i scr_84_get_lang_string(argc=1)
00427: pushi.e -5
00428: pushi.e 1
00429: pop.v.v [array]msg
00431: pushi.e 6
00432: conv.i.v
00433: pushi.e 50
00434: conv.i.v
00435: pushi.e 80
00436: conv.i.v
00437: call.i instance_create(argc=3)
00439: pop.v.v self.W
00441: pushi.e 16
00442: pop.v.i self.EVENT
00444: push.v self.EVENT
00446: pushi.e 15
00447: cmp.i.v EQ
00448: bf 00455
00449: pushi.e 6
00450: pushenv 00454
00451: call.i instance_destroy(argc=0)
00453: popz.v
00454: popenv 00451
00455: push.v self.EVENT
00457: pushi.e 16
00458: cmp.i.v EQ
00459: bf 00467
00460: pushi.e 6
00461: conv.i.v
00462: call.i instance_exists(argc=1)
00464: conv.v.b
00465: not.b
00466: b 00468
00467: push.e 0
00468: bf 00508
00469: pushi.e 667
00470: pop.v.i global.typer
00472: push.s ""DEVICE_CONTACT_slash_Step_0_gml_125_0""@9557
00474: conv.s.v
00475: call.i scr_84_get_lang_string(argc=1)
00477: pushi.e -5
00478: pushi.e 0
00479: pop.v.v [array]msg
00481: push.s ""DEVICE_CONTACT_slash_Step_0_gml_126_0""@9558
00483: conv.s.v
00484: call.i scr_84_get_lang_string(argc=1)
00486: pushi.e -5
00487: pushi.e 1
00488: pop.v.v [array]msg
00490: pushi.e 6
00491: conv.i.v
00492: pushi.e 40
00493: conv.i.v
00494: pushi.e 75
00495: conv.i.v
00496: call.i instance_create(argc=3)
00498: pop.v.v self.W
00500: pushi.e 17
00501: pop.v.i self.EVENT
00503: pushi.e 30
00504: pushi.e -1
00505: pushi.e 4
00506: pop.v.i [array]alarm
00508: push.v self.EVENT
00510: pushi.e 18
00511: cmp.i.v EQ
00512: bf 00526
00513: pushi.e 19
00514: pop.v.i self.EVENT
00516: pushi.e 312
00517: conv.i.v
00518: pushi.e 120
00519: conv.i.v
00520: pushi.e 140
00521: conv.i.v
00522: call.i instance_create(argc=3)
00524: pop.v.v self.GM
00526: push.v self.EVENT
00528: pushi.e 19
00529: cmp.i.v EQ
00530: bf 00548
00531: push.v self.GM
00533: call.i instance_exists(argc=1)
00535: conv.v.b
00536: not.b
00537: bf 00543
00538: push.d 19.1
00541: pop.v.d self.EVENT
00543: pushi.e 24
00544: pushi.e -1
00545: pushi.e 4
00546: pop.v.i [array]alarm
00548: push.v self.EVENT
00550: push.d 20.1
00553: cmp.d.v EQ
00554: bf 00606
00555: pushi.e 6
00556: pushenv 00560
00557: call.i instance_destroy(argc=0)
00559: popz.v
00560: popenv 00557
00561: push.s ""DEVICE_CONTACT_slash_Step_0_gml_148_0""@9560
00563: conv.s.v
00564: call.i scr_84_get_lang_string(argc=1)
00566: pushi.e -5
00567: pushi.e 0
00568: pop.v.v [array]msg
00570: push.s ""DEVICE_CONTACT_slash_Step_0_gml_149_0""@9561
00572: conv.s.v
00573: call.i scr_84_get_lang_string(argc=1)
00575: pushi.e -5
00576: pushi.e 1
00577: pop.v.v [array]msg
00579: push.s ""DEVICE_CONTACT_slash_Step_0_gml_150_0""@9562
00581: conv.s.v
00582: call.i scr_84_get_lang_string(argc=1)
00584: pushi.e -5
00585: pushi.e 2
00586: pop.v.v [array]msg
00588: pushi.e 6
00589: conv.i.v
00590: pushi.e 40
00591: conv.i.v
00592: pushi.e 75
00593: conv.i.v
00594: call.i instance_create(argc=3)
00596: pop.v.v self.W
00598: pushi.e 21
00599: pop.v.i self.EVENT
00601: pushi.e 30
00602: pushi.e -1
00603: pushi.e 4
00604: pop.v.i [array]alarm
00606: push.v self.EVENT
00608: pushi.e 22
00609: cmp.i.v EQ
00610: bf 00636
00611: pushi.e 312
00612: conv.i.v
00613: pushi.e 120
00614: conv.i.v
00615: pushi.e 140
00616: conv.i.v
00617: call.i instance_create(argc=3)
00619: pop.v.v self.GM
00621: pushi.e 1
00622: push.v self.GM
00624: conv.v.i
00625: pop.v.i [stacktop]s
00627: pushi.e 2
00628: push.v self.GM
00630: conv.v.i
00631: pop.v.i [stacktop]STEP
00633: pushi.e 23
00634: pop.v.i self.EVENT
00636: push.v self.EVENT
00638: pushi.e 23
00639: cmp.i.v EQ
00640: bf 00658
00641: push.v self.GM
00643: call.i instance_exists(argc=1)
00645: conv.v.b
00646: not.b
00647: bf 00653
00648: push.d 23.1
00651: pop.v.d self.EVENT
00653: pushi.e 24
00654: pushi.e -1
00655: pushi.e 4
00656: pop.v.i [array]alarm
00658: push.v self.EVENT
00660: push.d 24.1
00663: cmp.d.v EQ
00664: bf 00707
00665: pushi.e 6
00666: pushenv 00670
00667: call.i instance_destroy(argc=0)
00669: popz.v
00670: popenv 00667
00671: push.s ""DEVICE_CONTACT_slash_Step_0_gml_173_0""@9564
00673: conv.s.v
00674: call.i scr_84_get_lang_string(argc=1)
00676: pushi.e -5
00677: pushi.e 0
00678: pop.v.v [array]msg
00680: push.s ""DEVICE_CONTACT_slash_Step_0_gml_174_0""@9565
00682: conv.s.v
00683: call.i scr_84_get_lang_string(argc=1)
00685: pushi.e -5
00686: pushi.e 1
00687: pop.v.v [array]msg
00689: pushi.e 6
00690: conv.i.v
00691: pushi.e 40
00692: conv.i.v
00693: pushi.e 75
00694: conv.i.v
00695: call.i instance_create(argc=3)
00697: pop.v.v self.W
00699: pushi.e 25
00700: pop.v.i self.EVENT
00702: pushi.e 30
00703: pushi.e -1
00704: pushi.e 4
00705: pop.v.i [array]alarm
00707: push.v self.EVENT
00709: pushi.e 26
00710: cmp.i.v EQ
00711: bf 00737
00712: pushi.e 312
00713: conv.i.v
00714: pushi.e 120
00715: conv.i.v
00716: pushi.e 140
00717: conv.i.v
00718: call.i instance_create(argc=3)
00720: pop.v.v self.GM
00722: pushi.e 2
00723: push.v self.GM
00725: conv.v.i
00726: pop.v.i [stacktop]s
00728: pushi.e 3
00729: push.v self.GM
00731: conv.v.i
00732: pop.v.i [stacktop]STEP
00734: pushi.e 27
00735: pop.v.i self.EVENT
00737: push.v self.EVENT
00739: pushi.e 27
00740: cmp.i.v EQ
00741: bf 00757
00742: push.v self.GM
00744: call.i instance_exists(argc=1)
00746: conv.v.b
00747: not.b
00748: bf 00752
00749: pushi.e 28
00750: pop.v.i self.EVENT
00752: pushi.e 24
00753: pushi.e -1
00754: pushi.e 4
00755: pop.v.i [array]alarm
00757: push.v self.EVENT
00759: pushi.e 29
00760: cmp.i.v EQ
00761: bf 00850
00762: pushi.e 667
00763: pop.v.i global.typer
00765: pushi.e 6
00766: pushenv 00770
00767: call.i instance_destroy(argc=0)
00769: popz.v
00770: popenv 00767
00771: pushi.e 312
00772: conv.i.v
00773: pushi.e 90
00774: conv.i.v
00775: pushi.e 140
00776: conv.i.v
00777: call.i instance_create(argc=3)
00779: pop.v.v self.GM
00781: pushi.e 1
00782: push.v self.GM
00784: conv.v.i
00785: pop.v.i [stacktop]CANCEL
00787: pushi.e -1
00788: push.v self.GM
00790: conv.v.i
00791: pop.v.i [stacktop]FINISH
00793: pushi.e -1
00794: push.v self.GM
00796: conv.v.i
00797: pop.v.i [stacktop]s
00799: pushi.e 3
00800: push.v self.GM
00802: conv.v.i
00803: pop.v.i [stacktop]STEP
00805: push.s ""DEVICE_CONTACT_slash_Step_0_gml_205_0""@9567
00807: conv.s.v
00808: call.i scr_84_get_lang_string(argc=1)
00810: pushi.e -5
00811: pushi.e 0
00812: pop.v.v [array]msg
00814: push.s ""DEVICE_CONTACT_slash_Step_0_gml_206_0""@9568
00816: conv.s.v
00817: call.i scr_84_get_lang_string(argc=1)
00819: pushi.e -5
00820: pushi.e 1
00821: pop.v.v [array]msg
00823: push.s ""DEVICE_CONTACT_slash_Step_0_gml_207_0""@9569
00825: conv.s.v
00826: call.i scr_84_get_lang_string(argc=1)
00828: pushi.e -5
00829: pushi.e 2
00830: pop.v.v [array]msg
00832: pushi.e 6
00833: conv.i.v
00834: pushi.e 40
00835: conv.i.v
00836: pushi.e 60
00837: conv.i.v
00838: call.i instance_create(argc=3)
00840: pop.v.v self.W
00842: pushi.e 30
00843: pop.v.i self.EVENT
00845: pushi.e 110
00846: pushi.e -1
00847: pushi.e 4
00848: pop.v.i [array]alarm
00850: push.v self.EVENT
00852: pushi.e 31
00853: cmp.i.v EQ
00854: bf 00868
00855: pushi.e 309
00856: conv.i.v
00857: pushi.e 120
00858: conv.i.v
00859: pushi.e 100
00860: conv.i.v
00861: call.i instance_create(argc=3)
00863: pop.v.v self.choice
00865: pushi.e 32
00866: pop.v.i self.EVENT
00868: push.v self.EVENT
00870: pushi.e 32
00871: cmp.i.v EQ
00872: bf 00909
00873: pushglb.v global.choice
00875: pushi.e 0
00876: cmp.i.v EQ
00877: bt 00883
00878: pushglb.v global.choice
00880: pushi.e 1
00881: cmp.i.v EQ
00882: b 00884
00883: push.e 1
00884: bf 00909
00885: pushi.e 33
00886: pop.v.i self.EVENT
00888: pushglb.v global.choice
00890: pushi.e 1
00891: cmp.i.v EQ
00892: bf 00909
00893: push.v self.GM
00895: conv.v.i
00896: pushenv 00900
00897: pushi.e 1
00898: pop.v.i self.FINISH
00900: popenv 00897
00901: pushi.e 15
00902: pop.v.i self.EVENT
00904: pushi.e 20
00905: pushi.e -1
00906: pushi.e 4
00907: pop.v.i [array]alarm
00909: push.v self.EVENT
00911: pushi.e 33
00912: cmp.i.v EQ
00913: bf 00922
00914: pushi.e 34
00915: pop.v.i self.EVENT
00917: pushi.e 26
00918: pushi.e -1
00919: pushi.e 4
00920: pop.v.i [array]alarm
00922: push.v self.EVENT
00924: pushi.e 35
00925: cmp.i.v EQ
00926: bf 00982
00927: pushi.e 6
00928: pushenv 00932
00929: call.i instance_destroy(argc=0)
00931: popz.v
00932: popenv 00929
00933: push.s ""DEVICE_CONTACT_slash_Step_0_gml_240_0""@9570
00935: conv.s.v
00936: call.i scr_84_get_lang_string(argc=1)
00938: pushi.e -5
00939: pushi.e 0
00940: pop.v.v [array]msg
00942: push.s ""DEVICE_CONTACT_slash_Step_0_gml_241_0""@9571
00944: conv.s.v
00945: call.i scr_84_get_lang_string(argc=1)
00947: pushi.e -5
00948: pushi.e 1
00949: pop.v.v [array]msg
00951: push.s ""DEVICE_CONTACT_slash_Step_0_gml_242_0""@9572
00953: conv.s.v
00954: call.i scr_84_get_lang_string(argc=1)
00956: pushi.e -5
00957: pushi.e 2
00958: pop.v.v [array]msg
00960: push.s ""DEVICE_CONTACT_slash_Step_0_gml_243_0""@9573
00962: conv.s.v
00963: call.i scr_84_get_lang_string(argc=1)
00965: pushi.e -5
00966: pushi.e 3
00967: pop.v.v [array]msg
00969: pushi.e 6
00970: conv.i.v
00971: pushi.e 40
00972: conv.i.v
00973: pushi.e 60
00974: conv.i.v
00975: call.i instance_create(argc=3)
00977: pop.v.v self.W
00979: pushi.e 36
00980: pop.v.i self.EVENT
00982: push.v self.EVENT
00984: pushi.e 36
00985: cmp.i.v EQ
00986: bf 00994
00987: pushi.e 6
00988: conv.i.v
00989: call.i instance_exists(argc=1)
00991: conv.v.b
00992: not.b
00993: b 00995
00994: push.e 0
00995: bf 01038
00996: pushi.e 0
00997: pop.v.i self.GMSINE
00999: pushi.e 37
01000: pop.v.i self.EVENT
01002: pushi.e 30
01003: pushi.e -1
01004: pushi.e 4
01005: pop.v.i [array]alarm
01007: pushi.e 667
01008: pop.v.i global.typer
01010: push.s ""DEVICE_CONTACT_slash_Step_0_gml_258_0""@9575
01012: conv.s.v
01013: call.i scr_84_get_lang_string(argc=1)
01015: pushi.e -5
01016: pushi.e 0
01017: pop.v.v [array]msg
01019: push.s ""DEVICE_CONTACT_slash_Step_0_gml_259_0""@9576
01021: conv.s.v
01022: call.i scr_84_get_lang_string(argc=1)
01024: pushi.e -5
01025: pushi.e 1
01026: pop.v.v [array]msg
01028: pushi.e 6
01029: conv.i.v
01030: pushi.e 40
01031: conv.i.v
01032: pushi.e 80
01033: conv.i.v
01034: call.i instance_create(argc=3)
01036: pop.v.v self.W
01038: push.v self.EVENT
01040: pushi.e 37
01041: cmp.i.v EQ
01042: bf 01067
01043: push.v self.GMSINE
01045: pushi.e 1
01046: add.i.v
01047: pop.v.v self.GMSINE
01049: push.v self.GM
01051: conv.v.i
01052: dup.i 0
01053: push.v [stacktop]initx
01055: push.v self.GMSINE
01057: pushi.e 14
01058: conv.i.d
01059: div.d.v
01060: call.i sin(argc=1)
01062: pushi.e 1
01063: mul.i.v
01064: add.v.v
01065: pop.i.v [stacktop]initx
01067: push.v self.EVENT
01069: pushi.e 38
01070: cmp.i.v EQ
01071: bf 01326
01072: pushi.e 309
01073: conv.i.v
01074: pushi.e 0
01075: conv.i.v
01076: pushi.e 0
01077: conv.i.v
01078: call.i instance_create(argc=3)
01080: pop.v.v self.CHOICE
01082: pushi.e 39
01083: pop.v.i self.EVENT
01085: push.v self.CHOICE
01087: conv.v.i
01088: pushenv 01325
01089: pushi.e 2
01090: pop.v.i self.TYPE
01092: pushi.e 0
01093: pop.v.i self.i
01095: push.v self.i
01097: pushi.e 6
01098: cmp.i.v LTE
01099: bf 01189
01100: pushi.e 1
01101: push.v self.i
01103: add.v.i
01104: call.i string(argc=1)
01106: pushi.e -1
01107: pushi.e 0
01108: break.e -1
01109: push.i 32000
01111: mul.i.i
01112: push.v self.i
01114: conv.v.i
01115: break.e -1
01116: add.i.i
01117: pop.v.v [array]NAME
01119: pushi.e 80
01120: pushi.e -1
01121: pushi.e 0
01122: break.e -1
01123: push.i 32000
01125: mul.i.i
01126: push.v self.i
01128: conv.v.i
01129: break.e -1
01130: add.i.i
01131: pop.v.i [array]NAMEX
01133: pushglb.v global.lang
01135: push.s ""ja""@1566
01137: cmp.s.v EQ
01138: bf 01157
01139: pushi.e -1
01140: pushi.e 0
01141: break.e -1
01142: push.i 32000
01144: mul.i.i
01145: push.v self.i
01147: conv.v.i
01148: break.e -1
01149: add.i.i
01150: dup.i 1
01151: push.v [array]NAMEX
01153: pushi.e 16
01154: sub.i.v
01155: pop.i.v [array]NAMEX
01157: pushi.e 100
01158: push.v self.i
01160: pushi.e 16
01161: mul.i.v
01162: add.v.i
01163: pushi.e -1
01164: pushi.e 0
01165: break.e -1
01166: push.i 32000
01168: mul.i.i
01169: push.v self.i
01171: conv.v.i
01172: break.e -1
01173: add.i.i
01174: pop.v.v [array]NAMEY
01176: push.v self.YMAX
01178: pushi.e 1
01179: add.i.v
01180: pop.v.v self.YMAX
01182: push.v self.i
01184: pushi.e 1
01185: add.i.v
01186: pop.v.v self.i
01188: b 01095
01189: push.s ""DEVICE_CONTACT_slash_Step_0_gml_288_0""@9579
01191: conv.s.v
01192: call.i scr_84_get_lang_string(argc=1)
01194: pushi.e -1
01195: pushi.e 0
01196: break.e -1
01197: push.i 32000
01199: mul.i.i
01200: pushi.e 0
01201: break.e -1
01202: add.i.i
01203: pop.v.v [array]NAME
01205: push.s ""DEVICE_CONTACT_slash_Step_0_gml_289_0""@9580
01207: conv.s.v
01208: call.i scr_84_get_lang_string(argc=1)
01210: pushi.e -1
01211: pushi.e 0
01212: break.e -1
01213: push.i 32000
01215: mul.i.i
01216: pushi.e 1
01217: break.e -1
01218: add.i.i
01219: pop.v.v [array]NAME
01221: push.s ""DEVICE_CONTACT_slash_Step_0_gml_290_0""@9581
01223: conv.s.v
01224: call.i scr_84_get_lang_string(argc=1)
01226: pushi.e -1
01227: pushi.e 0
01228: break.e -1
01229: push.i 32000
01231: mul.i.i
01232: pushi.e 2
01233: break.e -1
01234: add.i.i
01235: pop.v.v [array]NAME
01237: push.s ""DEVICE_CONTACT_slash_Step_0_gml_291_0""@9582
01239: conv.s.v
01240: call.i scr_84_get_lang_string(argc=1)
01242: pushi.e -1
01243: pushi.e 0
01244: break.e -1
01245: push.i 32000
01247: mul.i.i
01248: pushi.e 3
01249: break.e -1
01250: add.i.i
01251: pop.v.v [array]NAME
01253: push.s ""DEVICE_CONTACT_slash_Step_0_gml_292_0""@9583
01255: conv.s.v
01256: call.i scr_84_get_lang_string(argc=1)
01258: pushi.e -1
01259: pushi.e 0
01260: break.e -1
01261: push.i 32000
01263: mul.i.i
01264: pushi.e 4
01265: break.e -1
01266: add.i.i
01267: pop.v.v [array]NAME
01269: push.s ""DEVICE_CONTACT_slash_Step_0_gml_293_0""@9584
01271: conv.s.v
01272: call.i scr_84_get_lang_string(argc=1)
01274: pushi.e -1
01275: pushi.e 0
01276: break.e -1
01277: push.i 32000
01279: mul.i.i
01280: pushi.e 5
01281: break.e -1
01282: add.i.i
01283: pop.v.v [array]NAME
01285: pushi.e 0
01286: pop.v.i self.CURX
01288: pushi.e -1
01289: pushi.e 0
01290: break.e -1
01291: push.i 32000
01293: mul.i.i
01294: pushi.e 0
01295: break.e -1
01296: add.i.i
01297: push.v [array]NAMEX
01299: pushi.e 20
01300: sub.i.v
01301: pop.v.v self.HEARTX
01303: pushi.e -1
01304: pushi.e 0
01305: break.e -1
01306: push.i 32000
01308: mul.i.i
01309: pushi.e 0
01310: break.e -1
01311: add.i.i
01312: push.v [array]NAMEY
01314: pop.v.v self.HEARTY
01316: pushi.e 0
01317: pop.v.i self.XMAX
01319: pushi.e 5
01320: pop.v.i self.YMAX
01322: pushi.e -20
01323: pop.v.i self.xoff
01325: popenv 01089
01326: push.v self.EVENT
01328: pushi.e 39
01329: cmp.i.v EQ
01330: bf 01350
01331: pushglb.v global.choice
01333: pushi.e -1
01334: cmp.i.v GT
01335: bf 01350
01336: pushglb.v global.choice
01338: pushi.e -5
01339: pushi.e 903
01340: pop.v.v [array]flag
01342: pushi.e 40
01343: pop.v.i self.EVENT
01345: pushi.e 26
01346: pushi.e -1
01347: pushi.e 4
01348: pop.v.i [array]alarm
01350: push.v self.EVENT
01352: pushi.e 41
01353: cmp.i.v EQ
01354: bf 01400
01355: pushi.e 6
01356: pushenv 01360
01357: call.i instance_destroy(argc=0)
01359: popz.v
01360: popenv 01357
01361: pushi.e 42
01362: pop.v.i self.EVENT
01364: pushi.e 30
01365: pushi.e -1
01366: pushi.e 4
01367: pop.v.i [array]alarm
01369: pushi.e 667
01370: pop.v.i global.typer
01372: push.s ""DEVICE_CONTACT_slash_Step_0_gml_323_0""@9585
01374: conv.s.v
01375: call.i scr_84_get_lang_string(argc=1)
01377: pushi.e -5
01378: pushi.e 0
01379: pop.v.v [array]msg
01381: push.s ""DEVICE_CONTACT_slash_Step_0_gml_324_0""@9586
01383: conv.s.v
01384: call.i scr_84_get_lang_string(argc=1)
01386: pushi.e -5
01387: pushi.e 1
01388: pop.v.v [array]msg
01390: pushi.e 6
01391: conv.i.v
01392: pushi.e 40
01393: conv.i.v
01394: pushi.e 80
01395: conv.i.v
01396: call.i instance_create(argc=3)
01398: pop.v.v self.W
01400: push.v self.EVENT
01402: pushi.e 43
01403: cmp.i.v EQ
01404: bf 01607
01405: pushi.e 309
01406: conv.i.v
01407: pushi.e 0
01408: conv.i.v
01409: pushi.e 0
01410: conv.i.v
01411: call.i instance_create(argc=3)
01413: pop.v.v self.CHOICE
01415: pushi.e 44
01416: pop.v.i self.EVENT
01418: push.v self.CHOICE
01420: conv.v.i
01421: pushenv 01606
01422: pushi.e 2
01423: pop.v.i self.TYPE
01425: pushi.e 0
01426: pop.v.i self.i
01428: push.v self.i
01430: pushi.e 5
01431: cmp.i.v LTE
01432: bf 01498
01433: pushi.e 1
01434: push.v self.i
01436: add.v.i
01437: call.i string(argc=1)
01439: pushi.e -1
01440: pushi.e 0
01441: break.e -1
01442: push.i 32000
01444: mul.i.i
01445: push.v self.i
01447: conv.v.i
01448: break.e -1
01449: add.i.i
01450: pop.v.v [array]NAME
01452: pushi.e 80
01453: pushi.e -1
01454: pushi.e 0
01455: break.e -1
01456: push.i 32000
01458: mul.i.i
01459: push.v self.i
01461: conv.v.i
01462: break.e -1
01463: add.i.i
01464: pop.v.i [array]NAMEX
01466: pushi.e 100
01467: push.v self.i
01469: pushi.e 16
01470: mul.i.v
01471: add.v.i
01472: pushi.e -1
01473: pushi.e 0
01474: break.e -1
01475: push.i 32000
01477: mul.i.i
01478: push.v self.i
01480: conv.v.i
01481: break.e -1
01482: add.i.i
01483: pop.v.v [array]NAMEY
01485: push.v self.YMAX
01487: pushi.e 1
01488: add.i.v
01489: pop.v.v self.YMAX
01491: push.v self.i
01493: pushi.e 1
01494: add.i.v
01495: pop.v.v self.i
01497: b 01428
01498: push.s ""A""@366
01500: pushi.e -1
01501: pushi.e 0
01502: break.e -1
01503: push.i 32000
01505: mul.i.i
01506: pushi.e 0
01507: break.e -1
01508: add.i.i
01509: pop.v.s [array]NAME
01511: push.s ""DEVICE_CONTACT_slash_Step_0_gml_344_0""@9587
01513: conv.s.v
01514: call.i scr_84_get_lang_string(argc=1)
01516: pushi.e -1
01517: pushi.e 0
01518: break.e -1
01519: push.i 32000
01521: mul.i.i
01522: pushi.e 1
01523: break.e -1
01524: add.i.i
01525: pop.v.v [array]NAME
01527: push.s ""B""@2529
01529: pushi.e -1
01530: pushi.e 0
01531: break.e -1
01532: push.i 32000
01534: mul.i.i
01535: pushi.e 2
01536: break.e -1
01537: add.i.i
01538: pop.v.s [array]NAME
01540: push.s ""C""@2530
01542: pushi.e -1
01543: pushi.e 0
01544: break.e -1
01545: push.i 32000
01547: mul.i.i
01548: pushi.e 3
01549: break.e -1
01550: add.i.i
01551: pop.v.s [array]NAME
01553: push.s ""D""@2531
01555: pushi.e -1
01556: pushi.e 0
01557: break.e -1
01558: push.i 32000
01560: mul.i.i
01561: pushi.e 4
01562: break.e -1
01563: add.i.i
01564: pop.v.s [array]NAME
01566: pushi.e 0
01567: pop.v.i self.CURX
01569: pushi.e -1
01570: pushi.e 0
01571: break.e -1
01572: push.i 32000
01574: mul.i.i
01575: pushi.e 0
01576: break.e -1
01577: add.i.i
01578: push.v [array]NAMEX
01580: pushi.e 20
01581: sub.i.v
01582: pop.v.v self.HEARTX
01584: pushi.e -1
01585: pushi.e 0
01586: break.e -1
01587: push.i 32000
01589: mul.i.i
01590: pushi.e 0
01591: break.e -1
01592: add.i.i
01593: push.v [array]NAMEY
01595: pop.v.v self.HEARTY
01597: pushi.e 0
01598: pop.v.i self.XMAX
01600: pushi.e 4
01601: pop.v.i self.YMAX
01603: pushi.e -20
01604: pop.v.i self.xoff
01606: popenv 01422
01607: push.v self.EVENT
01609: pushi.e 44
01610: cmp.i.v EQ
01611: bf 01631
01612: pushglb.v global.choice
01614: pushi.e -1
01615: cmp.i.v GT
01616: bf 01631
01617: pushglb.v global.choice
01619: pushi.e -5
01620: pushi.e 904
01621: pop.v.v [array]flag
01623: pushi.e 45
01624: pop.v.i self.EVENT
01626: pushi.e 26
01627: pushi.e -1
01628: pushi.e 4
01629: pop.v.i [array]alarm
01631: push.v self.EVENT
01633: pushi.e 46
01634: cmp.i.v EQ
01635: bf 01681
01636: pushi.e 6
01637: pushenv 01641
01638: call.i instance_destroy(argc=0)
01640: popz.v
01641: popenv 01638
01642: pushi.e 47
01643: pop.v.i self.EVENT
01645: pushi.e 30
01646: pushi.e -1
01647: pushi.e 4
01648: pop.v.i [array]alarm
01650: pushi.e 667
01651: pop.v.i global.typer
01653: push.s ""DEVICE_CONTACT_slash_Step_0_gml_381_0""@9588
01655: conv.s.v
01656: call.i scr_84_get_lang_string(argc=1)
01658: pushi.e -5
01659: pushi.e 0
01660: pop.v.v [array]msg
01662: push.s ""DEVICE_CONTACT_slash_Step_0_gml_382_0""@9589
01664: conv.s.v
01665: call.i scr_84_get_lang_string(argc=1)
01667: pushi.e -5
01668: pushi.e 1
01669: pop.v.v [array]msg
01671: pushi.e 6
01672: conv.i.v
01673: pushi.e 40
01674: conv.i.v
01675: pushi.e 80
01676: conv.i.v
01677: call.i instance_create(argc=3)
01679: pop.v.v self.W
01681: push.v self.EVENT
01683: pushi.e 48
01684: cmp.i.v EQ
01685: bf 01908
01686: pushi.e 309
01687: conv.i.v
01688: pushi.e 0
01689: conv.i.v
01690: pushi.e 0
01691: conv.i.v
01692: call.i instance_create(argc=3)
01694: pop.v.v self.CHOICE
01696: pushi.e 49
01697: pop.v.i self.EVENT
01699: push.v self.CHOICE
01701: conv.v.i
01702: pushenv 01907
01703: pushi.e 2
01704: pop.v.i self.TYPE
01706: pushi.e 0
01707: pop.v.i self.i
01709: push.v self.i
01711: pushi.e 3
01712: cmp.i.v LTE
01713: bf 01803
01714: pushi.e 1
01715: push.v self.i
01717: add.v.i
01718: call.i string(argc=1)
01720: pushi.e -1
01721: pushi.e 0
01722: break.e -1
01723: push.i 32000
01725: mul.i.i
01726: push.v self.i
01728: conv.v.i
01729: break.e -1
01730: add.i.i
01731: pop.v.v [array]NAME
01733: pushi.e 80
01734: pushi.e -1
01735: pushi.e 0
01736: break.e -1
01737: push.i 32000
01739: mul.i.i
01740: push.v self.i
01742: conv.v.i
01743: break.e -1
01744: add.i.i
01745: pop.v.i [array]NAMEX
01747: pushglb.v global.lang
01749: push.s ""ja""@1566
01751: cmp.s.v EQ
01752: bf 01771
01753: pushi.e -1
01754: pushi.e 0
01755: break.e -1
01756: push.i 32000
01758: mul.i.i
01759: push.v self.i
01761: conv.v.i
01762: break.e -1
01763: add.i.i
01764: dup.i 1
01765: push.v [array]NAMEX
01767: pushi.e 16
01768: sub.i.v
01769: pop.i.v [array]NAMEX
01771: pushi.e 100
01772: push.v self.i
01774: pushi.e 16
01775: mul.i.v
01776: add.v.i
01777: pushi.e -1
01778: pushi.e 0
01779: break.e -1
01780: push.i 32000
01782: mul.i.i
01783: push.v self.i
01785: conv.v.i
01786: break.e -1
01787: add.i.i
01788: pop.v.v [array]NAMEY
01790: push.v self.YMAX
01792: pushi.e 1
01793: add.i.v
01794: pop.v.v self.YMAX
01796: push.v self.i
01798: pushi.e 1
01799: add.i.v
01800: pop.v.v self.i
01802: b 01709
01803: push.s ""DEVICE_CONTACT_slash_Step_0_gml_401_0""@9590
01805: conv.s.v
01806: call.i scr_84_get_lang_string(argc=1)
01808: pushi.e -1
01809: pushi.e 0
01810: break.e -1
01811: push.i 32000
01813: mul.i.i
01814: pushi.e 0
01815: break.e -1
01816: add.i.i
01817: pop.v.v [array]NAME
01819: push.s ""DEVICE_CONTACT_slash_Step_0_gml_402_0""@9591
01821: conv.s.v
01822: call.i scr_84_get_lang_string(argc=1)
01824: pushi.e -1
01825: pushi.e 0
01826: break.e -1
01827: push.i 32000
01829: mul.i.i
01830: pushi.e 1
01831: break.e -1
01832: add.i.i
01833: pop.v.v [array]NAME
01835: push.s ""DEVICE_CONTACT_slash_Step_0_gml_403_0""@9592
01837: conv.s.v
01838: call.i scr_84_get_lang_string(argc=1)
01840: pushi.e -1
01841: pushi.e 0
01842: break.e -1
01843: push.i 32000
01845: mul.i.i
01846: pushi.e 2
01847: break.e -1
01848: add.i.i
01849: pop.v.v [array]NAME
01851: push.s ""DEVICE_CONTACT_slash_Step_0_gml_404_0""@9593
01853: conv.s.v
01854: call.i scr_84_get_lang_string(argc=1)
01856: pushi.e -1
01857: pushi.e 0
01858: break.e -1
01859: push.i 32000
01861: mul.i.i
01862: pushi.e 3
01863: break.e -1
01864: add.i.i
01865: pop.v.v [array]NAME
01867: pushi.e 0
01868: pop.v.i self.CURX
01870: pushi.e -1
01871: pushi.e 0
01872: break.e -1
01873: push.i 32000
01875: mul.i.i
01876: pushi.e 0
01877: break.e -1
01878: add.i.i
01879: push.v [array]NAMEX
01881: pushi.e 20
01882: sub.i.v
01883: pop.v.v self.HEARTX
01885: pushi.e -1
01886: pushi.e 0
01887: break.e -1
01888: push.i 32000
01890: mul.i.i
01891: pushi.e 0
01892: break.e -1
01893: add.i.i
01894: push.v [array]NAMEY
01896: pop.v.v self.HEARTY
01898: pushi.e 0
01899: pop.v.i self.XMAX
01901: pushi.e 3
01902: pop.v.i self.YMAX
01904: pushi.e -20
01905: pop.v.i self.xoff
01907: popenv 01703
01908: push.v self.EVENT
01910: pushi.e 49
01911: cmp.i.v EQ
01912: bf 01934
01913: pushglb.v global.choice
01915: pushi.e -1
01916: cmp.i.v GT
01917: bf 01934
01918: pushglb.v global.choice
01920: pushi.e -5
01921: pushi.e 905
01922: pop.v.v [array]flag
01924: push.d 50.5
01927: pop.v.d self.EVENT
01929: pushi.e 26
01930: pushi.e -1
01931: pushi.e 4
01932: pop.v.i [array]alarm
01934: push.v self.EVENT
01936: push.d 51.5
01939: cmp.d.v EQ
01940: bf 02002
01941: pushi.e 6
01942: pushenv 01946
01943: call.i instance_destroy(argc=0)
01945: popz.v
01946: popenv 01943
01947: push.d 52.5
01950: pop.v.d self.EVENT
01952: pushi.e 30
01953: pushi.e -1
01954: pushi.e 4
01955: pop.v.i [array]alarm
01957: pushi.e 667
01958: pop.v.i global.typer
01960: push.s ""DEVICE_CONTACT_slash_Step_0_gml_438_0""@9594
01962: conv.s.v
01963: call.i scr_84_get_lang_string(argc=1)
01965: pushi.e -5
01966: pushi.e 0
01967: pop.v.v [array]msg
01969: push.s ""DEVICE_CONTACT_slash_Step_0_gml_439_0""@9595
01971: conv.s.v
01972: call.i scr_84_get_lang_string(argc=1)
01974: pushi.e -5
01975: pushi.e 1
01976: pop.v.v [array]msg
01978: pushi.e 0
01979: pop.v.i self.JA_XOFF
01981: pushglb.v global.lang
01983: push.s ""ja""@1566
01985: cmp.s.v EQ
01986: bf 01990
01987: pushi.e -20
01988: pop.v.i self.JA_XOFF
01990: pushi.e 6
01991: conv.i.v
01992: pushi.e 30
01993: conv.i.v
01994: pushi.e 80
01995: push.v self.JA_XOFF
01997: add.v.i
01998: call.i instance_create(argc=3)
02000: pop.v.v self.W
02002: push.v self.EVENT
02004: push.d 53.5
02007: cmp.d.v EQ
02008: bf 02225
02009: pushi.e 309
02010: conv.i.v
02011: pushi.e 0
02012: conv.i.v
02013: pushi.e 0
02014: conv.i.v
02015: call.i instance_create(argc=3)
02017: pop.v.v self.CHOICE
02019: push.d 54.5
02022: pop.v.d self.EVENT
02024: push.v self.CHOICE
02026: conv.v.i
02027: pushenv 02224
02028: pushi.e 2
02029: pop.v.i self.TYPE
02031: pushi.e 0
02032: pop.v.i self.i
02034: push.v self.i
02036: pushi.e 4
02037: cmp.i.v LTE
02038: bf 02104
02039: pushi.e 1
02040: push.v self.i
02042: add.v.i
02043: call.i string(argc=1)
02045: pushi.e -1
02046: pushi.e 0
02047: break.e -1
02048: push.i 32000
02050: mul.i.i
02051: push.v self.i
02053: conv.v.i
02054: break.e -1
02055: add.i.i
02056: pop.v.v [array]NAME
02058: pushi.e 80
02059: pushi.e -1
02060: pushi.e 0
02061: break.e -1
02062: push.i 32000
02064: mul.i.i
02065: push.v self.i
02067: conv.v.i
02068: break.e -1
02069: add.i.i
02070: pop.v.i [array]NAMEX
02072: pushi.e 100
02073: push.v self.i
02075: pushi.e 16
02076: mul.i.v
02077: add.v.i
02078: pushi.e -1
02079: pushi.e 0
02080: break.e -1
02081: push.i 32000
02083: mul.i.i
02084: push.v self.i
02086: conv.v.i
02087: break.e -1
02088: add.i.i
02089: pop.v.v [array]NAMEY
02091: push.v self.YMAX
02093: pushi.e 1
02094: add.i.v
02095: pop.v.v self.YMAX
02097: push.v self.i
02099: pushi.e 1
02100: add.i.v
02101: pop.v.v self.i
02103: b 02034
02104: push.s ""DEVICE_CONTACT_slash_Step_0_gml_459_0""@9596
02106: conv.s.v
02107: call.i scr_84_get_lang_string(argc=1)
02109: pushi.e -1
02110: pushi.e 0
02111: break.e -1
02112: push.i 32000
02114: mul.i.i
02115: pushi.e 0
02116: break.e -1
02117: add.i.i
02118: pop.v.v [array]NAME
02120: push.s ""DEVICE_CONTACT_slash_Step_0_gml_460_0""@9597
02122: conv.s.v
02123: call.i scr_84_get_lang_string(argc=1)
02125: pushi.e -1
02126: pushi.e 0
02127: break.e -1
02128: push.i 32000
02130: mul.i.i
02131: pushi.e 1
02132: break.e -1
02133: add.i.i
02134: pop.v.v [array]NAME
02136: push.s ""DEVICE_CONTACT_slash_Step_0_gml_461_0""@9598
02138: conv.s.v
02139: call.i scr_84_get_lang_string(argc=1)
02141: pushi.e -1
02142: pushi.e 0
02143: break.e -1
02144: push.i 32000
02146: mul.i.i
02147: pushi.e 2
02148: break.e -1
02149: add.i.i
02150: pop.v.v [array]NAME
02152: push.s ""DEVICE_CONTACT_slash_Step_0_gml_462_0""@9599
02154: conv.s.v
02155: call.i scr_84_get_lang_string(argc=1)
02157: pushi.e -1
02158: pushi.e 0
02159: break.e -1
02160: push.i 32000
02162: mul.i.i
02163: pushi.e 3
02164: break.e -1
02165: add.i.i
02166: pop.v.v [array]NAME
02168: push.s ""DEVICE_CONTACT_slash_Step_0_gml_463_0""@9600
02170: conv.s.v
02171: call.i scr_84_get_lang_string(argc=1)
02173: pushi.e -1
02174: pushi.e 0
02175: break.e -1
02176: push.i 32000
02178: mul.i.i
02179: pushi.e 4
02180: break.e -1
02181: add.i.i
02182: pop.v.v [array]NAME
02184: pushi.e 0
02185: pop.v.i self.CURX
02187: pushi.e -1
02188: pushi.e 0
02189: break.e -1
02190: push.i 32000
02192: mul.i.i
02193: pushi.e 0
02194: break.e -1
02195: add.i.i
02196: push.v [array]NAMEX
02198: pushi.e 20
02199: sub.i.v
02200: pop.v.v self.HEARTX
02202: pushi.e -1
02203: pushi.e 0
02204: break.e -1
02205: push.i 32000
02207: mul.i.i
02208: pushi.e 0
02209: break.e -1
02210: add.i.i
02211: push.v [array]NAMEY
02213: pop.v.v self.HEARTY
02215: pushi.e 0
02216: pop.v.i self.XMAX
02218: pushi.e 4
02219: pop.v.i self.YMAX
02221: pushi.e -20
02222: pop.v.i self.xoff
02224: popenv 02028
02225: push.v self.EVENT
02227: push.d 54.5
02230: cmp.d.v EQ
02231: bf 02253
02232: pushglb.v global.choice
02234: pushi.e -1
02235: cmp.i.v GT
02236: bf 02253
02237: pushi.e 1
02238: pushglb.v global.choice
02240: sub.v.i
02241: pushi.e -5
02242: pushi.e 909
02243: pop.v.v [array]flag
02245: pushi.e 50
02246: pop.v.i self.EVENT
02248: pushi.e 26
02249: pushi.e -1
02250: pushi.e 4
02251: pop.v.i [array]alarm
02253: push.v self.EVENT
02255: pushi.e 51
02256: cmp.i.v EQ
02257: bf 02303
02258: pushi.e 6
02259: pushenv 02263
02260: call.i instance_destroy(argc=0)
02262: popz.v
02263: popenv 02260
02264: pushi.e 52
02265: pop.v.i self.EVENT
02267: pushi.e 30
02268: pushi.e -1
02269: pushi.e 4
02270: pop.v.i [array]alarm
02272: pushi.e 667
02273: pop.v.i global.typer
02275: push.s ""DEVICE_CONTACT_slash_Step_0_gml_497_0""@9601
02277: conv.s.v
02278: call.i scr_84_get_lang_string(argc=1)
02280: pushi.e -5
02281: pushi.e 0
02282: pop.v.v [array]msg
02284: push.s ""DEVICE_CONTACT_slash_Step_0_gml_498_0""@9602
02286: conv.s.v
02287: call.i scr_84_get_lang_string(argc=1)
02289: pushi.e -5
02290: pushi.e 1
02291: pop.v.v [array]msg
02293: pushi.e 6
02294: conv.i.v
02295: pushi.e 20
02296: conv.i.v
02297: pushi.e 50
02298: conv.i.v
02299: call.i instance_create(argc=3)
02301: pop.v.v self.W
02303: push.v self.EVENT
02305: pushi.e 53
02306: cmp.i.v EQ
02307: bf 02506
02308: pushi.e 309
02309: conv.i.v
02310: pushi.e 0
02311: conv.i.v
02312: pushi.e 0
02313: conv.i.v
02314: call.i instance_create(argc=3)
02316: pop.v.v self.CHOICE
02318: pushi.e 54
02319: pop.v.i self.EVENT
02321: push.v self.CHOICE
02323: conv.v.i
02324: pushenv 02505
02325: pushi.e 2
02326: pop.v.i self.TYPE
02328: pushi.e 0
02329: pop.v.i self.i
02331: push.v self.i
02333: pushi.e 3
02334: cmp.i.v LTE
02335: bf 02401
02336: pushi.e 1
02337: push.v self.i
02339: add.v.i
02340: call.i string(argc=1)
02342: pushi.e -1
02343: pushi.e 0
02344: break.e -1
02345: push.i 32000
02347: mul.i.i
02348: push.v self.i
02350: conv.v.i
02351: break.e -1
02352: add.i.i
02353: pop.v.v [array]NAME
02355: pushi.e 80
02356: pushi.e -1
02357: pushi.e 0
02358: break.e -1
02359: push.i 32000
02361: mul.i.i
02362: push.v self.i
02364: conv.v.i
02365: break.e -1
02366: add.i.i
02367: pop.v.i [array]NAMEX
02369: pushi.e 100
02370: push.v self.i
02372: pushi.e 16
02373: mul.i.v
02374: add.v.i
02375: pushi.e -1
02376: pushi.e 0
02377: break.e -1
02378: push.i 32000
02380: mul.i.i
02381: push.v self.i
02383: conv.v.i
02384: break.e -1
02385: add.i.i
02386: pop.v.v [array]NAMEY
02388: push.v self.YMAX
02390: pushi.e 1
02391: add.i.v
02392: pop.v.v self.YMAX
02394: push.v self.i
02396: pushi.e 1
02397: add.i.v
02398: pop.v.v self.i
02400: b 02331
02401: push.s ""DEVICE_CONTACT_slash_Step_0_gml_518_0""@9603
02403: conv.s.v
02404: call.i scr_84_get_lang_string(argc=1)
02406: pushi.e -1
02407: pushi.e 0
02408: break.e -1
02409: push.i 32000
02411: mul.i.i
02412: pushi.e 0
02413: break.e -1
02414: add.i.i
02415: pop.v.v [array]NAME
02417: push.s ""DEVICE_CONTACT_slash_Step_0_gml_519_0""@9604
02419: conv.s.v
02420: call.i scr_84_get_lang_string(argc=1)
02422: pushi.e -1
02423: pushi.e 0
02424: break.e -1
02425: push.i 32000
02427: mul.i.i
02428: pushi.e 1
02429: break.e -1
02430: add.i.i
02431: pop.v.v [array]NAME
02433: push.s ""DEVICE_CONTACT_slash_Step_0_gml_520_0""@9605
02435: conv.s.v
02436: call.i scr_84_get_lang_string(argc=1)
02438: pushi.e -1
02439: pushi.e 0
02440: break.e -1
02441: push.i 32000
02443: mul.i.i
02444: pushi.e 2
02445: break.e -1
02446: add.i.i
02447: pop.v.v [array]NAME
02449: push.s ""DEVICE_CONTACT_slash_Step_0_gml_521_0""@9606
02451: conv.s.v
02452: call.i scr_84_get_lang_string(argc=1)
02454: pushi.e -1
02455: pushi.e 0
02456: break.e -1
02457: push.i 32000
02459: mul.i.i
02460: pushi.e 3
02461: break.e -1
02462: add.i.i
02463: pop.v.v [array]NAME
02465: pushi.e 0
02466: pop.v.i self.CURX
02468: pushi.e -1
02469: pushi.e 0
02470: break.e -1
02471: push.i 32000
02473: mul.i.i
02474: pushi.e 0
02475: break.e -1
02476: add.i.i
02477: push.v [array]NAMEX
02479: pushi.e 20
02480: sub.i.v
02481: pop.v.v self.HEARTX
02483: pushi.e -1
02484: pushi.e 0
02485: break.e -1
02486: push.i 32000
02488: mul.i.i
02489: pushi.e 0
02490: break.e -1
02491: add.i.i
02492: push.v [array]NAMEY
02494: pop.v.v self.HEARTY
02496: pushi.e 0
02497: pop.v.i self.XMAX
02499: pushi.e 3
02500: pop.v.i self.YMAX
02502: pushi.e -20
02503: pop.v.i self.xoff
02505: popenv 02325
02506: push.v self.EVENT
02508: pushi.e 54
02509: cmp.i.v EQ
02510: bf 02532
02511: pushglb.v global.choice
02513: pushi.e -1
02514: cmp.i.v GT
02515: bf 02532
02516: pushglb.v global.choice
02518: pushi.e -5
02519: pushi.e 906
02520: pop.v.v [array]flag
02522: push.d 54.1
02525: pop.v.d self.EVENT
02527: pushi.e 26
02528: pushi.e -1
02529: pushi.e 4
02530: pop.v.i [array]alarm
02532: push.v self.EVENT
02534: push.d 55.1
02537: cmp.d.v EQ
02538: bf 02584
02539: pushi.e 6
02540: pushenv 02544
02541: call.i instance_destroy(argc=0)
02543: popz.v
02544: popenv 02541
02545: pushi.e 56
02546: pop.v.i self.EVENT
02548: pushi.e 30
02549: pushi.e -1
02550: pushi.e 4
02551: pop.v.i [array]alarm
02553: pushi.e 667
02554: pop.v.i global.typer
02556: push.s ""DEVICE_CONTACT_slash_Step_0_gml_555_0""@9607
02558: conv.s.v
02559: call.i scr_84_get_lang_string(argc=1)
02561: pushi.e -5
02562: pushi.e 0
02563: pop.v.v [array]msg
02565: push.s ""DEVICE_CONTACT_slash_Step_0_gml_556_0""@9608
02567: conv.s.v
02568: call.i scr_84_get_lang_string(argc=1)
02570: pushi.e -5
02571: pushi.e 1
02572: pop.v.v [array]msg
02574: pushi.e 6
02575: conv.i.v
02576: pushi.e 30
02577: conv.i.v
02578: pushi.e 65
02579: conv.i.v
02580: call.i instance_create(argc=3)
02582: pop.v.v self.W
02584: push.v self.EVENT
02586: pushi.e 57
02587: cmp.i.v EQ
02588: bf 02755
02589: pushi.e 309
02590: conv.i.v
02591: pushi.e 0
02592: conv.i.v
02593: pushi.e 0
02594: conv.i.v
02595: call.i instance_create(argc=3)
02597: pop.v.v self.CHOICE
02599: pushi.e 58
02600: pop.v.i self.EVENT
02602: push.v self.CHOICE
02604: conv.v.i
02605: pushenv 02754
02606: pushi.e 2
02607: pop.v.i self.TYPE
02609: pushi.e 0
02610: pop.v.i self.i
02612: push.v self.i
02614: pushi.e 1
02615: cmp.i.v LTE
02616: bf 02682
02617: pushi.e 1
02618: push.v self.i
02620: add.v.i
02621: call.i string(argc=1)
02623: pushi.e -1
02624: pushi.e 0
02625: break.e -1
02626: push.i 32000
02628: mul.i.i
02629: push.v self.i
02631: conv.v.i
02632: break.e -1
02633: add.i.i
02634: pop.v.v [array]NAME
02636: pushi.e 80
02637: pushi.e -1
02638: pushi.e 0
02639: break.e -1
02640: push.i 32000
02642: mul.i.i
02643: push.v self.i
02645: conv.v.i
02646: break.e -1
02647: add.i.i
02648: pop.v.i [array]NAMEX
02650: pushi.e 100
02651: push.v self.i
02653: pushi.e 16
02654: mul.i.v
02655: add.v.i
02656: pushi.e -1
02657: pushi.e 0
02658: break.e -1
02659: push.i 32000
02661: mul.i.i
02662: push.v self.i
02664: conv.v.i
02665: break.e -1
02666: add.i.i
02667: pop.v.v [array]NAMEY
02669: push.v self.YMAX
02671: pushi.e 1
02672: add.i.v
02673: pop.v.v self.YMAX
02675: push.v self.i
02677: pushi.e 1
02678: add.i.v
02679: pop.v.v self.i
02681: b 02612
02682: push.s ""DEVICE_CONTACT_slash_Step_0_gml_575_0""@9609
02684: conv.s.v
02685: call.i scr_84_get_lang_string(argc=1)
02687: pushi.e -1
02688: pushi.e 0
02689: break.e -1
02690: push.i 32000
02692: mul.i.i
02693: pushi.e 0
02694: break.e -1
02695: add.i.i
02696: pop.v.v [array]NAME
02698: push.s ""DEVICE_CONTACT_slash_Step_0_gml_576_0""@9610
02700: conv.s.v
02701: call.i scr_84_get_lang_string(argc=1)
02703: pushi.e -1
02704: pushi.e 0
02705: break.e -1
02706: push.i 32000
02708: mul.i.i
02709: pushi.e 1
02710: break.e -1
02711: add.i.i
02712: pop.v.v [array]NAME
02714: pushi.e 0
02715: pop.v.i self.CURX
02717: pushi.e -1
02718: pushi.e 0
02719: break.e -1
02720: push.i 32000
02722: mul.i.i
02723: pushi.e 0
02724: break.e -1
02725: add.i.i
02726: push.v [array]NAMEX
02728: pushi.e 20
02729: sub.i.v
02730: pop.v.v self.HEARTX
02732: pushi.e -1
02733: pushi.e 0
02734: break.e -1
02735: push.i 32000
02737: mul.i.i
02738: pushi.e 0
02739: break.e -1
02740: add.i.i
02741: push.v [array]NAMEY
02743: pop.v.v self.HEARTY
02745: pushi.e 0
02746: pop.v.i self.XMAX
02748: pushi.e 1
02749: pop.v.i self.YMAX
02751: pushi.e -20
02752: pop.v.i self.xoff
02754: popenv 02606
02755: push.v self.EVENT
02757: pushi.e 58
02758: cmp.i.v EQ
02759: bf 02779
02760: pushglb.v global.choice
02762: pushi.e -1
02763: cmp.i.v GT
02764: bf 02779
02765: pushglb.v global.choice
02767: pushi.e -5
02768: pushi.e 907
02769: pop.v.v [array]flag
02771: pushi.e 59
02772: pop.v.i self.EVENT
02774: pushi.e 26
02775: pushi.e -1
02776: pushi.e 4
02777: pop.v.i [array]alarm
02779: push.v self.EVENT
02781: pushi.e 60
02782: cmp.i.v EQ
02783: bf 02829
02784: pushi.e 6
02785: pushenv 02789
02786: call.i instance_destroy(argc=0)
02788: popz.v
02789: popenv 02786
02790: pushi.e 61
02791: pop.v.i self.EVENT
02793: pushi.e 30
02794: pushi.e -1
02795: pushi.e 4
02796: pop.v.i [array]alarm
02798: pushi.e 667
02799: pop.v.i global.typer
02801: push.s ""DEVICE_CONTACT_slash_Step_0_gml_611_0""@9611
02803: conv.s.v
02804: call.i scr_84_get_lang_string(argc=1)
02806: pushi.e -5
02807: pushi.e 0
02808: pop.v.v [array]msg
02810: push.s ""DEVICE_CONTACT_slash_Step_0_gml_612_0""@9612
02812: conv.s.v
02813: call.i scr_84_get_lang_string(argc=1)
02815: pushi.e -5
02816: pushi.e 1
02817: pop.v.v [array]msg
02819: pushi.e 6
02820: conv.i.v
02821: pushi.e 20
02822: conv.i.v
02823: pushi.e 60
02824: conv.i.v
02825: call.i instance_create(argc=3)
02827: pop.v.v self.W
02829: push.v self.EVENT
02831: pushi.e 62
02832: cmp.i.v EQ
02833: bf 03000
02834: pushi.e 309
02835: conv.i.v
02836: pushi.e 0
02837: conv.i.v
02838: pushi.e 0
02839: conv.i.v
02840: call.i instance_create(argc=3)
02842: pop.v.v self.CHOICE
02844: pushi.e 63
02845: pop.v.i self.EVENT
02847: push.v self.CHOICE
02849: conv.v.i
02850: pushenv 02999
02851: pushi.e 2
02852: pop.v.i self.TYPE
02854: pushi.e 0
02855: pop.v.i self.i
02857: push.v self.i
02859: pushi.e 1
02860: cmp.i.v LTE
02861: bf 02927
02862: pushi.e 1
02863: push.v self.i
02865: add.v.i
02866: call.i string(argc=1)
02868: pushi.e -1
02869: pushi.e 0
02870: break.e -1
02871: push.i 32000
02873: mul.i.i
02874: push.v self.i
02876: conv.v.i
02877: break.e -1
02878: add.i.i
02879: pop.v.v [array]NAME
02881: pushi.e 80
02882: pushi.e -1
02883: pushi.e 0
02884: break.e -1
02885: push.i 32000
02887: mul.i.i
02888: push.v self.i
02890: conv.v.i
02891: break.e -1
02892: add.i.i
02893: pop.v.i [array]NAMEX
02895: pushi.e 100
02896: push.v self.i
02898: pushi.e 16
02899: mul.i.v
02900: add.v.i
02901: pushi.e -1
02902: pushi.e 0
02903: break.e -1
02904: push.i 32000
02906: mul.i.i
02907: push.v self.i
02909: conv.v.i
02910: break.e -1
02911: add.i.i
02912: pop.v.v [array]NAMEY
02914: push.v self.YMAX
02916: pushi.e 1
02917: add.i.v
02918: pop.v.v self.YMAX
02920: push.v self.i
02922: pushi.e 1
02923: add.i.v
02924: pop.v.v self.i
02926: b 02857
02927: push.s ""DEVICE_CONTACT_slash_Step_0_gml_631_0""@9613
02929: conv.s.v
02930: call.i scr_84_get_lang_string(argc=1)
02932: pushi.e -1
02933: pushi.e 0
02934: break.e -1
02935: push.i 32000
02937: mul.i.i
02938: pushi.e 0
02939: break.e -1
02940: add.i.i
02941: pop.v.v [array]NAME
02943: push.s ""DEVICE_CONTACT_slash_Step_0_gml_632_0""@9614
02945: conv.s.v
02946: call.i scr_84_get_lang_string(argc=1)
02948: pushi.e -1
02949: pushi.e 0
02950: break.e -1
02951: push.i 32000
02953: mul.i.i
02954: pushi.e 1
02955: break.e -1
02956: add.i.i
02957: pop.v.v [array]NAME
02959: pushi.e 0
02960: pop.v.i self.CURX
02962: pushi.e -1
02963: pushi.e 0
02964: break.e -1
02965: push.i 32000
02967: mul.i.i
02968: pushi.e 0
02969: break.e -1
02970: add.i.i
02971: push.v [array]NAMEX
02973: pushi.e 20
02974: sub.i.v
02975: pop.v.v self.HEARTX
02977: pushi.e -1
02978: pushi.e 0
02979: break.e -1
02980: push.i 32000
02982: mul.i.i
02983: pushi.e 0
02984: break.e -1
02985: add.i.i
02986: push.v [array]NAMEY
02988: pop.v.v self.HEARTY
02990: pushi.e 0
02991: pop.v.i self.XMAX
02993: pushi.e 1
02994: pop.v.i self.YMAX
02996: pushi.e -20
02997: pop.v.i self.xoff
02999: popenv 02851
03000: push.v self.EVENT
03002: pushi.e 63
03003: cmp.i.v EQ
03004: bf 03024
03005: pushglb.v global.choice
03007: pushi.e -1
03008: cmp.i.v GT
03009: bf 03024
03010: pushglb.v global.choice
03012: pushi.e -5
03013: pushi.e 908
03014: pop.v.v [array]flag
03016: pushi.e 64
03017: pop.v.i self.EVENT
03019: pushi.e 26
03020: pushi.e -1
03021: pushi.e 4
03022: pop.v.i [array]alarm
03024: push.v self.EVENT
03026: pushi.e 65
03027: cmp.i.v EQ
03028: bf 03084
03029: pushi.e 6
03030: pushenv 03034
03031: call.i instance_destroy(argc=0)
03033: popz.v
03034: popenv 03031
03035: push.s ""DEVICE_CONTACT_slash_Step_0_gml_664_0""@9615
03037: conv.s.v
03038: call.i scr_84_get_lang_string(argc=1)
03040: pushi.e -5
03041: pushi.e 0
03042: pop.v.v [array]msg
03044: pushi.e 6
03045: conv.i.v
03046: pushi.e 50
03047: conv.i.v
03048: pushi.e 90
03049: conv.i.v
03050: call.i instance_create(argc=3)
03052: pop.v.v self.W
03054: push.d 65.5
03057: pop.v.d self.EVENT
03059: pushi.e 32
03060: pushi.e -1
03061: pushi.e 4
03062: pop.v.i [array]alarm
03064: pushi.e 312
03065: conv.i.v
03066: call.i instance_exists(argc=1)
03068: conv.v.b
03069: bf 03084
03070: push.v self.GM
03072: conv.v.i
03073: push.v [stacktop]initx
03075: pop.v.v self.gmx
03077: push.v self.GM
03079: conv.v.i
03080: push.v [stacktop]inity
03082: pop.v.v self.gmy
03084: push.v self.EVENT
03086: push.d 65.5
03089: cmp.d.v EQ
03090: bf 03139
03091: pushi.e 312
03092: conv.i.v
03093: call.i instance_exists(argc=1)
03095: conv.v.b
03096: bf 03139
03097: push.v self.GM
03099: conv.v.i
03100: push.v [stacktop]initx
03102: push.v self.gmx
03104: pushi.e 24
03105: sub.i.v
03106: cmp.v.v GT
03107: bf 03118
03108: push.v self.GM
03110: conv.v.i
03111: dup.i 0
03112: push.v [stacktop]initx
03114: pushi.e 1
03115: sub.i.v
03116: pop.i.v [stacktop]initx
03118: push.v self.GM
03120: conv.v.i
03121: push.v [stacktop]inity
03123: push.v self.gmy
03125: pushi.e 56
03126: add.i.v
03127: cmp.v.v LT
03128: bf 03139
03129: push.v self.GM
03131: conv.v.i
03132: dup.i 0
03133: push.v [stacktop]inity
03135: pushi.e 2
03136: add.i.v
03137: pop.i.v [stacktop]inity
03139: push.v self.EVENT
03141: push.d 66.5
03144: cmp.d.v EQ
03145: bf 03153
03146: pushi.e 6
03147: conv.i.v
03148: call.i instance_exists(argc=1)
03150: pushi.e 0
03151: cmp.i.v EQ
03152: b 03154
03153: push.e 0
03154: bf 03243
03155: pushi.e 0
03156: pop.v.i self.JA_XOFF
03158: pushglb.v global.lang
03160: push.s ""ja""@1566
03162: cmp.s.v EQ
03163: bf 03167
03164: pushi.e 30
03165: pop.v.i self.JA_XOFF
03167: pushi.e 6
03168: pushenv 03172
03169: call.i instance_destroy(argc=0)
03171: popz.v
03172: popenv 03169
03173: push.s ""DEVICE_CONTACT_slash_Step_0_gml_697_0""@9619
03175: conv.s.v
03176: call.i scr_84_get_lang_string(argc=1)
03178: pushi.e -5
03179: pushi.e 0
03180: pop.v.v [array]msg
03182: push.s ""DEVICE_CONTACT_slash_Step_0_gml_698_0""@9620
03184: conv.s.v
03185: call.i scr_84_get_lang_string(argc=1)
03187: pushi.e -5
03188: pushi.e 1
03189: pop.v.v [array]msg
03191: pushi.e 6
03192: conv.i.v
03193: pushi.e 20
03194: conv.i.v
03195: pushi.e 68
03196: push.v self.JA_XOFF
03198: add.v.i
03199: call.i instance_create(argc=3)
03201: pop.v.v self.W
03203: pushi.e 309
03204: conv.i.v
03205: pushi.e 0
03206: conv.i.v
03207: pushi.e 0
03208: conv.i.v
03209: call.i instance_create(argc=3)
03211: pop.v.v self.CHOICE
03213: pushi.e 67
03214: pop.v.i self.EVENT
03216: push.v self.CHOICE
03218: conv.v.i
03219: pushenv 03225
03220: pushi.e 0
03221: conv.i.v
03222: call.i event_user(argc=1)
03224: popz.v
03225: popenv 03220
03226: push.v self.CHOICE
03228: conv.v.i
03229: pushenv 03242
03230: pushi.e 9
03231: pop.v.i self.STRINGMAX
03233: pushglb.v global.lang
03235: push.s ""ja""@1566
03237: cmp.s.v EQ
03238: bf 03242
03239: pushi.e 7
03240: pop.v.i self.STRINGMAX
03242: popenv 03230
03243: push.v self.EVENT
03245: pushi.e 67
03246: cmp.i.v EQ
03247: bf 03307
03248: push.v self.CHOICE
03250: call.i instance_exists(argc=1)
03252: conv.v.b
03253: bf 03299
03254: push.v self.CHOICE
03256: conv.v.i
03257: push.v [stacktop]NAMESTRING
03259: pop.v.v global.name
03261: pushglb.v global.name
03263: pop.v.v self.FN_2
03265: pushglb.v global.lang
03267: push.s ""ja""@1566
03269: cmp.s.v EQ
03270: bf 03286
03271: push.v self.FN_2
03273: push.s ""ＧＡＳＴＥＲ""@9622
03275: cmp.s.v EQ
03276: bf 03280
03277: call.i game_restart(argc=0)
03279: popz.v
03280: push.v self.FN_2
03282: call.i string_to_hiragana(argc=1)
03284: pop.v.v self.FN_2
03286: push.v self.FN_2
03288: push.s ""DEVICE_CONTACT_slash_Step_0_gml_714_0""@9624
03290: conv.s.v
03291: call.i scr_84_get_lang_string(argc=1)
03293: cmp.v.v EQ
03294: bf 03298
03295: call.i game_restart(argc=0)
03297: popz.v
03298: b 03307
03299: pushi.e 68
03300: pop.v.i self.EVENT
03302: pushi.e 26
03303: pushi.e -1
03304: pushi.e 4
03305: pop.v.i [array]alarm
03307: push.v self.EVENT
03309: pushi.e 69
03310: cmp.i.v EQ
03311: bf 03408
03312: pushi.e 6
03313: pushenv 03317
03314: call.i instance_destroy(argc=0)
03316: popz.v
03317: popenv 03314
03318: pushi.e 0
03319: pop.v.i self.FOUND
03321: pushglb.v global.name
03323: pop.v.v self.FN
03325: pushi.e 0
03326: conv.i.v
03327: call.i event_user(argc=1)
03329: popz.v
03330: pushglb.v global.name
03332: push.s ""DEVICE_CONTACT_slash_Step_0_gml_729_0""@9627
03334: conv.s.v
03335: call.i scr_84_get_lang_string(argc=1)
03337: call.i scr_84_get_subst_string(argc=2)
03339: pushi.e -5
03340: pushi.e 0
03341: pop.v.v [array]msg
03343: push.s ""DEVICE_CONTACT_slash_Step_0_gml_730_0""@9628
03345: conv.s.v
03346: call.i scr_84_get_lang_string(argc=1)
03348: pushi.e -5
03349: pushi.e 1
03350: pop.v.v [array]msg
03352: push.v self.FOUND
03354: pushi.e 1
03355: cmp.i.v EQ
03356: bt 03362
03357: push.v self.FOUND
03359: pushi.e 2
03360: cmp.i.v EQ
03361: b 03363
03362: push.e 1
03363: bf 03395
03364: pushglb.v global.name
03366: push.s ""DEVICE_CONTACT_slash_Step_0_gml_734_0""@9629
03368: conv.s.v
03369: call.i scr_84_get_lang_string(argc=1)
03371: call.i scr_84_get_subst_string(argc=2)
03373: pushi.e -5
03374: pushi.e 0
03375: pop.v.v [array]msg
03377: push.s ""DEVICE_CONTACT_slash_Step_0_gml_735_0""@9630
03379: conv.s.v
03380: call.i scr_84_get_lang_string(argc=1)
03382: pushi.e -5
03383: pushi.e 1
03384: pop.v.v [array]msg
03386: push.s ""DEVICE_CONTACT_slash_Step_0_gml_736_0""@9631
03388: conv.s.v
03389: call.i scr_84_get_lang_string(argc=1)
03391: pushi.e -5
03392: pushi.e 2
03393: pop.v.v [array]msg
03395: pushi.e 6
03396: conv.i.v
03397: pushi.e 50
03398: conv.i.v
03399: pushi.e 80
03400: conv.i.v
03401: call.i instance_create(argc=3)
03403: pop.v.v self.W
03405: pushi.e 70
03406: pop.v.i self.EVENT
03408: push.v self.EVENT
03410: pushi.e 70
03411: cmp.i.v EQ
03412: bf 03420
03413: pushi.e 6
03414: conv.i.v
03415: call.i instance_exists(argc=1)
03417: pushi.e 0
03418: cmp.i.v EQ
03419: b 03421
03420: push.e 0
03421: bf 03507
03422: pushi.e 0
03423: pop.v.i self.JA_XOFF
03425: pushglb.v global.lang
03427: push.s ""ja""@1566
03429: cmp.s.v EQ
03430: bf 03434
03431: pushi.e -32
03432: pop.v.i self.JA_XOFF
03434: pushi.e 6
03435: pushenv 03439
03436: call.i instance_destroy(argc=0)
03438: popz.v
03439: popenv 03436
03440: push.s ""DEVICE_CONTACT_slash_Step_0_gml_755_0""@9632
03442: conv.s.v
03443: call.i scr_84_get_lang_string(argc=1)
03445: pushi.e -5
03446: pushi.e 0
03447: pop.v.v [array]msg
03449: push.s ""DEVICE_CONTACT_slash_Step_0_gml_756_0""@9633
03451: conv.s.v
03452: call.i scr_84_get_lang_string(argc=1)
03454: pushi.e -5
03455: pushi.e 1
03456: pop.v.v [array]msg
03458: pushi.e 6
03459: conv.i.v
03460: pushi.e 20
03461: conv.i.v
03462: pushi.e 88
03463: push.v self.JA_XOFF
03465: add.v.i
03466: call.i instance_create(argc=3)
03468: pop.v.v self.W
03470: pushi.e 309
03471: conv.i.v
03472: pushi.e 0
03473: conv.i.v
03474: pushi.e 0
03475: conv.i.v
03476: call.i instance_create(argc=3)
03478: pop.v.v self.CHOICE
03480: pushi.e 71
03481: pop.v.i self.EVENT
03483: push.v self.CHOICE
03485: conv.v.i
03486: pushenv 03492
03487: pushi.e 0
03488: conv.i.v
03489: call.i event_user(argc=1)
03491: popz.v
03492: popenv 03487
03493: push.v self.CHOICE
03495: conv.v.i
03496: pushenv 03506
03497: pushglb.v global.lang
03499: push.s ""ja""@1566
03501: cmp.s.v EQ
03502: bf 03506
03503: pushi.e 7
03504: pop.v.i self.STRINGMAX
03506: popenv 03497
03507: push.v self.EVENT
03509: pushi.e 71
03510: cmp.i.v EQ
03511: bf 03570
03512: push.v self.CHOICE
03514: call.i instance_exists(argc=1)
03516: conv.v.b
03517: bf 03562
03518: push.v self.CHOICE
03520: conv.v.i
03521: push.v [stacktop]NAMESTRING
03523: pop.v.v global.truename
03525: pushglb.v global.truename
03527: pop.v.v self.FN_3
03529: pushglb.v global.lang
03531: push.s ""ja""@1566
03533: cmp.s.v EQ
03534: bf 03541
03535: push.v self.FN_3
03537: call.i string_to_hiragana(argc=1)
03539: pop.v.v self.FN_3
03541: push.v self.FN_3
03543: push.s ""DEVICE_CONTACT_slash_Step_0_gml_770_0""@9635
03545: conv.s.v
03546: call.i scr_84_get_lang_string(argc=1)
03548: cmp.v.v EQ
03549: bt 03556
03550: push.v self.FN_3
03552: push.s ""ＧＡＳＴＥＲ""@9622
03554: cmp.s.v EQ
03555: b 03557
03556: push.e 1
03557: bf 03561
03558: call.i ossafe_game_end(argc=0)
03560: popz.v
03561: b 03570
03562: pushi.e 72
03563: pop.v.i self.EVENT
03565: pushi.e 26
03566: pushi.e -1
03567: pushi.e 4
03568: pop.v.i [array]alarm
03570: push.v self.EVENT
03572: pushi.e 73
03573: cmp.i.v EQ
03574: bf 03698
03575: pushi.e 6
03576: pushenv 03580
03577: call.i instance_destroy(argc=0)
03579: popz.v
03580: popenv 03577
03581: pushi.e 0
03582: pop.v.i self.FOUND
03584: pushglb.v global.truename
03586: pop.v.v self.FN
03588: pushi.e 0
03589: conv.i.v
03590: call.i event_user(argc=1)
03592: popz.v
03593: pushglb.v global.truename
03595: push.s ""DEVICE_CONTACT_slash_Step_0_gml_785_0""@9636
03597: conv.s.v
03598: call.i scr_84_get_lang_string(argc=1)
03600: call.i scr_84_get_subst_string(argc=2)
03602: pushi.e -5
03603: pushi.e 0
03604: pop.v.v [array]msg
03606: push.s ""DEVICE_CONTACT_slash_Step_0_gml_786_0""@9637
03608: conv.s.v
03609: call.i scr_84_get_lang_string(argc=1)
03611: pushi.e -5
03612: pushi.e 1
03613: pop.v.v [array]msg
03615: push.s ""DEVICE_CONTACT_slash_Step_0_gml_787_0""@9638
03617: conv.s.v
03618: call.i scr_84_get_lang_string(argc=1)
03620: pushi.e -5
03621: pushi.e 2
03622: pop.v.v [array]msg
03624: push.v self.FOUND
03626: pushi.e 1
03627: cmp.i.v EQ
03628: bf 03638
03629: push.s ""DEVICE_CONTACT_slash_Step_0_gml_790_0""@9639
03631: conv.s.v
03632: call.i scr_84_get_lang_string(argc=1)
03634: pushi.e -5
03635: pushi.e 1
03636: pop.v.v [array]msg
03638: push.v self.FOUND
03640: pushi.e 2
03641: cmp.i.v EQ
03642: bf 03661
03643: push.s ""DEVICE_CONTACT_slash_Step_0_gml_794_0""@9640
03645: conv.s.v
03646: call.i scr_84_get_lang_string(argc=1)
03648: pushi.e -5
03649: pushi.e 1
03650: pop.v.v [array]msg
03652: push.s ""DEVICE_CONTACT_slash_Step_0_gml_795_0""@9641
03654: conv.s.v
03655: call.i scr_84_get_lang_string(argc=1)
03657: pushi.e -5
03658: pushi.e 2
03659: pop.v.v [array]msg
03661: pushglb.v global.name
03663: pushglb.v global.truename
03665: cmp.v.v EQ
03666: bf 03685
03667: push.s ""DEVICE_CONTACT_slash_Step_0_gml_800_0""@9642
03669: conv.s.v
03670: call.i scr_84_get_lang_string(argc=1)
03672: pushi.e -5
03673: pushi.e 1
03674: pop.v.v [array]msg
03676: push.s ""DEVICE_CONTACT_slash_Step_0_gml_801_0""@9643
03678: conv.s.v
03679: call.i scr_84_get_lang_string(argc=1)
03681: pushi.e -5
03682: pushi.e 2
03683: pop.v.v [array]msg
03685: pushi.e 6
03686: conv.i.v
03687: pushi.e 50
03688: conv.i.v
03689: pushi.e 80
03690: conv.i.v
03691: call.i instance_create(argc=3)
03693: pop.v.v self.W
03695: pushi.e 74
03696: pop.v.i self.EVENT
03698: push.v self.EVENT
03700: pushi.e 74
03701: cmp.i.v EQ
03702: bf 03710
03703: pushi.e 6
03704: conv.i.v
03705: call.i instance_exists(argc=1)
03707: conv.v.b
03708: not.b
03709: b 03711
03710: push.e 0
03711: bf 03774
03712: pushglb.v global.name
03714: pushi.e -5
03715: pushi.e 0
03716: pop.v.v [array]othername
03718: pushi.e 667
03719: pop.v.i global.typer
03721: pushglb.v global.truename
03723: push.s ""DEVICE_CONTACT_slash_Step_0_gml_816_0""@9644
03725: conv.s.v
03726: call.i scr_84_get_lang_string(argc=1)
03728: call.i scr_84_get_subst_string(argc=2)
03730: pushi.e -5
03731: pushi.e 0
03732: pop.v.v [array]msg
03734: push.s ""DEVICE_CONTACT_slash_Step_0_gml_817_0""@9645
03736: conv.s.v
03737: call.i scr_84_get_lang_string(argc=1)
03739: pushi.e -5
03740: pushi.e 1
03741: pop.v.v [array]msg
03743: push.s ""DEVICE_CONTACT_slash_Step_0_gml_818_0""@9646
03745: conv.s.v
03746: call.i scr_84_get_lang_string(argc=1)
03748: pushi.e -5
03749: pushi.e 2
03750: pop.v.v [array]msg
03752: push.s ""DEVICE_CONTACT_slash_Step_0_gml_819_0""@9647
03754: conv.s.v
03755: call.i scr_84_get_lang_string(argc=1)
03757: pushi.e -5
03758: pushi.e 3
03759: pop.v.v [array]msg
03761: pushi.e 6
03762: conv.i.v
03763: pushi.e 50
03764: conv.i.v
03765: pushi.e 80
03766: conv.i.v
03767: call.i instance_create(argc=3)
03769: pop.v.v self.W
03771: pushi.e 75
03772: pop.v.i self.EVENT
03774: push.v self.EVENT
03776: pushi.e 75
03777: cmp.i.v EQ
03778: bf 03786
03779: pushi.e 6
03780: conv.i.v
03781: call.i instance_exists(argc=1)
03783: conv.v.b
03784: not.b
03785: b 03787
03786: push.e 0
03787: bf 03825
03788: call.i snd_free_all(argc=0)
03790: popz.v
03791: pushi.e 13
03792: conv.i.v
03793: call.i snd_play(argc=1)
03795: popz.v
03796: pushi.e 312
03797: pushenv 03801
03798: call.i instance_destroy(argc=0)
03800: popz.v
03801: popenv 03798
03802: pushi.e 314
03803: pushenv 03807
03804: call.i instance_destroy(argc=0)
03806: popz.v
03807: popenv 03804
03808: pushi.e 0
03809: pop.v.i self.OBMADE
03811: pushi.e 76
03812: pop.v.i self.EVENT
03814: push.s "" ""@24
03816: conv.s.v
03817: call.i scr_windowcaption(argc=1)
03819: popz.v
03820: pushi.e 30
03821: pushi.e -1
03822: pushi.e 4
03823: pop.v.i [array]alarm
03825: push.v self.EVENT
03827: pushi.e 77
03828: cmp.i.v EQ
03829: bf 03878
03830: pushi.e 2
03831: pop.v.i global.typer
03833: push.s ""DEVICE_CONTACT_slash_Step_0_gml_839_0""@9648
03835: conv.s.v
03836: call.i scr_84_get_lang_string(argc=1)
03838: pushi.e -5
03839: pushi.e 0
03840: pop.v.v [array]msg
03842: push.s ""DEVICE_CONTACT_slash_Step_0_gml_840_0""@9649
03844: conv.s.v
03845: call.i scr_84_get_lang_string(argc=1)
03847: pushi.e -5
03848: pushi.e 1
03849: pop.v.v [array]msg
03851: pushi.e 0
03852: pop.v.i self.JA_XOFF
03854: pushglb.v global.lang
03856: push.s ""ja""@1566
03858: cmp.s.v EQ
03859: bf 03863
03860: pushi.e -10
03861: pop.v.i self.JA_XOFF
03863: pushi.e 6
03864: conv.i.v
03865: pushi.e 50
03866: conv.i.v
03867: pushi.e 100
03868: push.v self.JA_XOFF
03870: add.v.i
03871: call.i instance_create(argc=3)
03873: pop.v.v self.W
03875: pushi.e 78
03876: pop.v.i self.EVENT
03878: push.v self.EVENT
03880: pushi.e 78
03881: cmp.i.v EQ
03882: bf 03890
03883: pushi.e 6
03884: conv.i.v
03885: call.i instance_exists(argc=1)
03887: conv.v.b
03888: not.b
03889: b 03891
03890: push.e 0
03891: bf 03998
03892: pushi.e 2
03893: pop.v.i global.typer
03895: pushglb.v global.lang
03897: push.s ""ja""@1566
03899: cmp.s.v EQ
03900: bf 03904
03901: pushi.e 60
03902: pop.v.i global.typer
03904: push.s ""w.ogg""@9650
03906: conv.s.v
03907: call.i snd_init(argc=1)
03909: pushi.e -5
03910: pushi.e 0
03911: pop.v.v [array]currentsong
03913: pushi.e -5
03914: pushi.e 0
03915: push.v [array]currentsong
03917: call.i mus_loop(argc=1)
03919: pop.v.v self.loop1
03921: pushi.e -5
03922: pushi.e 0
03923: push.v [array]currentsong
03925: call.i mus_loop(argc=1)
03927: pop.v.v self.loop2
03929: pushi.e 0
03930: conv.i.v
03931: push.v self.loop1
03933: call.i snd_pitch(argc=2)
03935: popz.v
03936: pushi.e 0
03937: conv.i.v
03938: push.v self.loop2
03940: call.i snd_pitch(argc=2)
03942: popz.v
03943: pushi.e 0
03944: pop.v.i self.p
03946: push.s ""DEVICE_CONTACT_slash_Step_0_gml_854_0""@9653
03948: conv.s.v
03949: call.i scr_84_get_lang_string(argc=1)
03951: pushi.e -5
03952: pushi.e 0
03953: pop.v.v [array]msg
03955: pushi.e 0
03956: pop.v.i self.JA_XOFF
03958: pushglb.v global.lang
03960: push.s ""ja""@1566
03962: cmp.s.v EQ
03963: bf 03967
03964: pushi.e -15
03965: pop.v.i self.JA_XOFF
03967: pushi.e 6
03968: conv.i.v
03969: pushi.e 50
03970: conv.i.v
03971: pushi.e 145
03972: push.v self.JA_XOFF
03974: add.v.i
03975: call.i instance_create(argc=3)
03977: pop.v.v self.W
03979: pushi.e 98
03980: pop.v.i self.EVENT
03982: pushi.e 1
03983: pop.v.i self.WHITEFADE
03985: push.d 0.008
03988: pop.v.d self.FADEUP
03990: push.d -0.1
03993: pop.v.d self.FADEFACTOR
03995: pushi.e -20
03996: pop.v.i self.depth
03998: push.v self.EVENT
04000: pushi.e 99
04001: cmp.i.v EQ
04002: bf 04013
04003: pushi.e 0
04004: pushi.e -5
04005: pushi.e 6
04006: pop.v.i [array]flag
04008: pushi.e 2
04009: conv.i.v
04010: call.i room_goto(argc=1)
04012: popz.v
04013: push.v self.EVENT
04015: pushi.e 98
04016: cmp.i.v EQ
04017: bf 04057
04018: push.v self.p
04020: push.d 0.008
04023: add.d.v
04024: pop.v.v self.p
04026: push.v self.p
04028: push.v self.loop1
04030: call.i snd_pitch(argc=2)
04032: popz.v
04033: push.v self.p
04035: push.d 1.2
04038: mul.d.v
04039: push.v self.loop2
04041: call.i snd_pitch(argc=2)
04043: popz.v
04044: push.v self.p
04046: push.d 1.5
04049: cmp.d.v GTE
04050: bf 04057
04051: pushi.e 99
04052: pop.v.i self.EVENT
04054: call.i snd_free_all(argc=0)
04056: popz.v
04057: push.v self.EVENT
04059: pushi.e 100
04060: cmp.i.v EQ
04061: bf 04069
04062: pushi.e 6
04063: conv.i.v
04064: call.i instance_exists(argc=1)
04066: conv.v.b
04067: not.b
04068: b 04070
04069: push.e 0
04070: bf 04134
04071: push.s ""w.ogg""@9650
04073: conv.s.v
04074: call.i snd_init(argc=1)
04076: pushi.e -5
04077: pushi.e 0
04078: pop.v.v [array]currentsong
04080: pushi.e -5
04081: pushi.e 0
04082: push.v [array]currentsong
04084: call.i mus_loop(argc=1)
04086: popz.v
04087: pushi.e 101
04088: pop.v.i self.EVENT
04090: pushi.e 999
04091: pop.v.i global.typer
04093: push.s ""DEVICE_CONTACT_slash_Step_0_gml_889_0""@9654
04095: conv.s.v
04096: call.i scr_84_get_lang_string(argc=1)
04098: pushi.e -5
04099: pushi.e 0
04100: pop.v.v [array]msg
04102: push.s ""DEVICE_CONTACT_slash_Step_0_gml_890_0""@9655
04104: conv.s.v
04105: call.i scr_84_get_lang_string(argc=1)
04107: pushi.e -5
04108: pushi.e 1
04109: pop.v.v [array]msg
04111: push.s ""DEVICE_CONTACT_slash_Step_0_gml_891_0""@9656
04113: conv.s.v
04114: call.i scr_84_get_lang_string(argc=1)
04116: pushi.e -5
04117: pushi.e 2
04118: pop.v.v [array]msg
04120: pushi.e 350
04121: pushi.e -1
04122: pushi.e 4
04123: pop.v.i [array]alarm
04125: pushi.e 6
04126: conv.i.v
04127: pushi.e 90
04128: conv.i.v
04129: pushi.e 125
04130: conv.i.v
04131: call.i instance_create(argc=3)
04133: popz.v
04134: push.v self.EVENT
04136: pushi.e 102
04137: cmp.i.v EQ
04138: bf 04144
04139: pushi.e 2
04140: conv.i.v
04141: call.i room_goto(argc=1)
04143: popz.v
04144: push.v self.EVENT
04146: pushi.e 900
04147: cmp.i.v EQ
04148: bf 04197
04149: pushi.e 667
04150: pop.v.i global.typer
04152: push.s ""DEVICE_CONTACT_slash_Step_0_gml_928_0""@9657
04154: conv.s.v
04155: call.i scr_84_get_lang_string(argc=1)
04157: pushi.e -5
04158: pushi.e 0
04159: pop.v.v [array]msg
04161: push.s ""DEVICE_CONTACT_slash_Step_0_gml_929_0""@9658
04163: conv.s.v
04164: call.i scr_84_get_lang_string(argc=1)
04166: pushi.e -5
04167: pushi.e 1
04168: pop.v.v [array]msg
04170: push.s ""DEVICE_CONTACT_slash_Step_0_gml_930_0""@9659
04172: conv.s.v
04173: call.i scr_84_get_lang_string(argc=1)
04175: pushi.e -5
04176: pushi.e 2
04177: pop.v.v [array]msg
04179: pushi.e 6
04180: conv.i.v
04181: pushi.e 50
04182: conv.i.v
04183: pushi.e 80
04184: conv.i.v
04185: call.i instance_create(argc=3)
04187: pop.v.v self.W
04189: pushi.e 919
04190: pop.v.i self.EVENT
04192: pushi.e 100
04193: pushi.e -1
04194: pushi.e 4
04195: pop.v.i [array]alarm
04197: push.v self.EVENT
04199: pushi.e 920
04200: cmp.i.v EQ
04201: bf 04215
04202: pushi.e 309
04203: conv.i.v
04204: pushi.e 100
04205: conv.i.v
04206: pushi.e 100
04207: conv.i.v
04208: call.i instance_create(argc=3)
04210: pop.v.v self.choice
04212: pushi.e 930
04213: pop.v.i self.EVENT
04215: push.v self.EVENT
04217: pushi.e 930
04218: cmp.i.v EQ
04219: bf 04240
04220: pushglb.v global.choice
04222: pushi.e 1
04223: cmp.i.v EQ
04224: bt 04230
04225: pushglb.v global.choice
04227: pushi.e 0
04228: cmp.i.v EQ
04229: b 04231
04230: push.e 1
04231: bf 04235
04232: pushi.e 940
04233: pop.v.i self.EVENT
04235: pushi.e 60
04236: pushi.e -1
04237: pushi.e 4
04238: pop.v.i [array]alarm
04240: push.v self.HEARTMADE
04242: pushi.e 1
04243: cmp.i.v EQ
04244: bf 04271
04245: push.v self.HSINER
04247: pushi.e 1
04248: add.i.v
04249: pop.v.v self.HSINER
04251: push.v self.SOUL
04253: conv.v.i
04254: push.v [stacktop]ystart
04256: push.v self.HSINER
04258: pushi.e 16
04259: conv.i.d
04260: div.d.v
04261: call.i sin(argc=1)
04263: pushi.e 2
04264: mul.i.v
04265: add.v.v
04266: push.v self.SOUL
04268: conv.v.i
04269: pop.v.v [stacktop]y
04271: pushi.e -5
04272: pushi.e 20
04273: push.v [array]flag
04275: pushi.e 0
04276: cmp.i.v EQ
04277: bf 04284
04278: pushi.e 6
04279: pushenv 04283
04280: pushi.e 1
04281: pop.v.i self.specfade
04283: popenv 04280
04284: pushi.e -5
04285: pushi.e 20
04286: push.v [array]flag
04288: pushi.e 1
04289: cmp.i.v EQ
04290: bf 04318
04291: pushi.e 6
04292: pushenv 04301
04293: push.v self.specfade
04295: push.d 0.025
04298: sub.d.v
04299: pop.v.v self.specfade
04301: popenv 04293
04302: push.v self.EVENT
04304: pushi.e 16
04305: cmp.i.v GTE
04306: bf 04318
04307: pushi.e 6
04308: pushenv 04317
04309: push.v self.specfade
04311: push.d 0.01
04314: sub.d.v
04315: pop.v.v self.specfade
04317: popenv 04309
04318: push.v self.OBMADE
04320: pushi.e 1
04321: cmp.i.v EQ
04322: bf 04383
04323: push.v self.OB_DEPTH
04325: pushi.e 1
04326: add.i.v
04327: pop.v.v self.OB_DEPTH
04329: push.v self.obacktimer
04331: push.v self.OBM
04333: add.v.v
04334: pop.v.v self.obacktimer
04336: push.v self.obacktimer
04338: pushi.e 20
04339: cmp.i.v GTE
04340: bf 04383
04341: pushi.e 314
04342: conv.i.v
04343: pushi.e 0
04344: conv.i.v
04345: pushi.e 0
04346: conv.i.v
04347: call.i instance_create(argc=3)
04349: pop.v.v self.DV
04351: pushi.e 5
04352: push.v self.OB_DEPTH
04354: add.v.i
04355: push.v self.DV
04357: conv.v.i
04358: pop.v.v [stacktop]depth
04360: push.d 0.01
04363: push.v self.OBM
04365: mul.v.d
04366: push.v self.DV
04368: conv.v.i
04369: pop.v.v [stacktop]OBSPEED
04371: push.v self.OB_DEPTH
04373: push.i 60000
04375: cmp.i.v GTE
04376: bf 04380
04377: pushi.e 0
04378: pop.v.i self.OB_DEPTH
04380: pushi.e 0
04381: pop.v.i self.obacktimer
04383: push.v self.SKIPBUFFER
04385: pushi.e 1
04386: sub.i.v
04387: pop.v.v self.SKIPBUFFER
04389: push.v self.ALREADY
04391: pushi.e 1
04392: cmp.i.v EQ
04393: bf 04483
04394: call.i button2_h(argc=0)
04396: pushi.e 1
04397: cmp.i.v EQ
04398: bf 04409
04399: push.v self.SKIPBUFFER
04401: pushi.e 0
04402: cmp.i.v LT
04403: bf 04409
04404: push.v self.EVENT
04406: pushi.e 75
04407: cmp.i.v LTE
04408: b 04410
04409: push.e 0
04410: bf 04483
04411: pushi.e 6
04412: pushenv 04462
04413: push.v self.pos
04415: push.v self.length
04417: pushi.e 3
04418: sub.i.v
04419: cmp.v.v LT
04420: bf 04427
04421: push.v self.pos
04423: pushi.e 2
04424: add.i.v
04425: pop.v.v self.pos
04427: pushi.e -1
04428: pushi.e 0
04429: push.v [array]alarm
04431: pushi.e 10
04432: cmp.i.v GTE
04433: bf 04439
04434: pushi.e 10
04435: pushi.e -1
04436: pushi.e 0
04437: pop.v.i [array]alarm
04439: push.v self.specfade
04441: push.d 0.9
04444: cmp.d.v LTE
04445: bf 04454
04446: push.v self.specfade
04448: push.d 0.1
04451: sub.d.v
04452: pop.v.v self.specfade
04454: push.v self.rate
04456: pushi.e 1
04457: cmp.i.v LTE
04458: bf 04462
04459: pushi.e 1
04460: pop.v.i self.rate
04462: popenv 04413
04463: push.v self.EVENT
04465: pushi.e 15
04466: cmp.i.v GTE
04467: bf 04480
04468: pushi.e -1
04469: pushi.e 4
04470: push.v [array]alarm
04472: pushi.e 6
04473: cmp.i.v GTE
04474: bf 04480
04475: pushi.e 6
04476: pushi.e -1
04477: pushi.e 4
04478: pop.v.i [array]alarm
04480: pushi.e 1
04481: pop.v.i self.SKIPBUFFER
04483: call.i scr_debug(argc=0)
04485: conv.v.b
04486: bf func_end
04487: pushi.e 8
04488: conv.i.v
04489: call.i keyboard_check_pressed(argc=1)
04491: conv.v.b
04492: bf func_end
04493: pushi.e 0
04494: pushi.e -5
04495: pushi.e 6
04496: pop.v.i [array]flag
04498: call.i snd_free_all(argc=0)
04500: popz.v
04501: pushi.e 2
04502: conv.i.v
04503: call.i room_goto(argc=1)
04505: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_credits").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.v self.timer
00002: pushi.e 1
00003: add.i.v
00004: pop.v.v self.timer
00006: push.v self.timer
00008: pushi.e 1
00009: cmp.i.v EQ
00010: bf 00024
00011: push.s ""dontforget.ogg""@9885
00013: conv.s.v
00014: call.i snd_init(argc=1)
00016: pop.v.v self.song0
00018: push.v self.song0
00020: call.i mus_play(argc=1)
00022: pop.v.v self.song1
00024: push.v self.timer
00026: pushi.e 60
00027: cmp.i.v EQ
00028: bf 00036
00029: push.s ""obj_credits_slash_Step_0_gml_13_0""@9888
00031: conv.s.v
00032: call.i scr_84_get_lang_string(argc=1)
00034: pop.v.v self.lyric
00036: push.v self.timer
00038: pushi.e 108
00039: cmp.i.v EQ
00040: bf 00081
00041: push.s ""obj_credits_slash_Step_0_gml_19_0""@9889
00043: conv.s.v
00044: call.i scr_84_get_lang_string(argc=1)
00046: pop.v.v self.lyric
00048: push.s ""obj_credits_slash_Step_0_gml_21_0""@9890
00050: conv.s.v
00051: call.i scr_84_get_lang_string(argc=1)
00053: pushi.e -1
00054: pushi.e 0
00055: pop.v.v [array]line
00057: push.s ""obj_credits_slash_Step_0_gml_22_0""@9891
00059: conv.s.v
00060: call.i scr_84_get_lang_string(argc=1)
00062: pushi.e -1
00063: pushi.e 1
00064: pop.v.v [array]line
00066: push.s "" ""@24
00068: pushi.e -1
00069: pushi.e 2
00070: pop.v.s [array]line
00072: push.s ""obj_credits_slash_Step_0_gml_24_0""@9892
00074: conv.s.v
00075: call.i scr_84_get_lang_string(argc=1)
00077: pushi.e -1
00078: pushi.e 3
00079: pop.v.v [array]line
00081: push.v self.timer
00083: pushi.e 180
00084: cmp.i.v EQ
00085: bf 00099
00086: pushglb.v global.lang
00088: push.s ""en""@2775
00090: cmp.s.v EQ
00091: bf 00099
00092: push.s ""obj_credits_slash_Step_0_gml_33_0""@9893
00094: conv.s.v
00095: call.i scr_84_get_lang_string(argc=1)
00097: pop.v.v self.lyric
00099: push.v self.timer
00101: pushi.e 201
00102: cmp.i.v EQ
00103: bf 00183
00104: push.s ""obj_credits_slash_Step_0_gml_38_0""@9894
00106: conv.s.v
00107: call.i scr_84_get_lang_string(argc=1)
00109: pushi.e -1
00110: pushi.e 0
00111: pop.v.v [array]line
00113: push.s ""obj_credits_slash_Step_0_gml_39_0""@9895
00115: conv.s.v
00116: call.i scr_84_get_lang_string(argc=1)
00118: pushi.e -1
00119: pushi.e 1
00120: pop.v.v [array]line
00122: push.s ""obj_credits_slash_Step_0_gml_40_0""@9896
00124: conv.s.v
00125: call.i scr_84_get_lang_string(argc=1)
00127: pushi.e -1
00128: pushi.e 2
00129: pop.v.v [array]line
00131: push.s "" ""@24
00133: pushi.e -1
00134: pushi.e 3
00135: pop.v.s [array]line
00137: push.s ""obj_credits_slash_Step_0_gml_42_0""@9897
00139: conv.s.v
00140: call.i scr_84_get_lang_string(argc=1)
00142: pushi.e -1
00143: pushi.e 4
00144: pop.v.v [array]line
00146: push.i 12632256
00148: pushi.e -1
00149: pushi.e 0
00150: pop.v.i [array]linecolor
00152: push.i 12632256
00154: pushi.e -1
00155: pushi.e 1
00156: pop.v.i [array]linecolor
00158: push.i 12632256
00160: pushi.e -1
00161: pushi.e 2
00162: pop.v.i [array]linecolor
00164: push.i 16777215
00166: pushi.e -1
00167: pushi.e 4
00168: pop.v.i [array]linecolor
00170: pushglb.v global.lang
00172: push.s ""ja""@1566
00174: cmp.s.v EQ
00175: bf 00183
00176: push.s ""obj_credits_slash_Step_0_gml_33_0""@9893
00178: conv.s.v
00179: call.i scr_84_get_lang_string(argc=1)
00181: pop.v.v self.lyric
00183: push.v self.timer
00185: pushi.e 278
00186: cmp.i.v EQ
00187: bf 00201
00188: pushglb.v global.lang
00190: push.s ""en""@2775
00192: cmp.s.v EQ
00193: bf 00201
00194: push.s ""obj_credits_slash_Step_0_gml_54_0""@9898
00196: conv.s.v
00197: call.i scr_84_get_lang_string(argc=1)
00199: pop.v.v self.lyric
00201: push.v self.timer
00203: pushi.e 298
00204: cmp.i.v EQ
00205: bf 00267
00206: push.s ""obj_credits_slash_Step_0_gml_59_0""@9899
00208: conv.s.v
00209: call.i scr_84_get_lang_string(argc=1)
00211: pushi.e -1
00212: pushi.e 0
00213: pop.v.v [array]line
00215: push.s ""obj_credits_slash_Step_0_gml_60_0""@9900
00217: conv.s.v
00218: call.i scr_84_get_lang_string(argc=1)
00220: pushi.e -1
00221: pushi.e 1
00222: pop.v.v [array]line
00224: push.s ""obj_credits_slash_Step_0_gml_61_0""@9901
00226: conv.s.v
00227: call.i scr_84_get_lang_string(argc=1)
00229: pushi.e -1
00230: pushi.e 2
00231: pop.v.v [array]line
00233: push.i 12632256
00235: pushi.e -1
00236: pushi.e 2
00237: pop.v.i [array]linecolor
00239: push.s "" ""@24
00241: pushi.e -1
00242: pushi.e 3
00243: pop.v.s [array]line
00245: push.s ""obj_credits_slash_Step_0_gml_64_0""@9902
00247: conv.s.v
00248: call.i scr_84_get_lang_string(argc=1)
00250: pushi.e -1
00251: pushi.e 4
00252: pop.v.v [array]line
00254: pushglb.v global.lang
00256: push.s ""ja""@1566
00258: cmp.s.v EQ
00259: bf 00267
00260: push.s ""obj_credits_slash_Step_0_gml_54_0""@9898
00262: conv.s.v
00263: call.i scr_84_get_lang_string(argc=1)
00265: pop.v.v self.lyric
00267: push.v self.timer
00269: pushi.e 366
00270: cmp.i.v EQ
00271: bf 00285
00272: pushglb.v global.lang
00274: push.s ""en""@2775
00276: cmp.s.v EQ
00277: bf 00285
00278: push.s ""obj_credits_slash_Step_0_gml_70_0""@9903
00280: conv.s.v
00281: call.i scr_84_get_lang_string(argc=1)
00283: pop.v.v self.lyric
00285: push.v self.timer
00287: pushi.e 390
00288: cmp.i.v EQ
00289: bf 00369
00290: push.s ""obj_credits_slash_Step_0_gml_95_0""@9904
00292: conv.s.v
00293: call.i scr_84_get_lang_string(argc=1)
00295: pushi.e -1
00296: pushi.e 0
00297: pop.v.v [array]line
00299: push.s ""obj_credits_slash_Step_0_gml_96_0""@9905
00301: conv.s.v
00302: call.i scr_84_get_lang_string(argc=1)
00304: pushi.e -1
00305: pushi.e 1
00306: pop.v.v [array]line
00308: push.s "" ""@24
00310: pushi.e -1
00311: pushi.e 2
00312: pop.v.s [array]line
00314: push.s ""obj_credits_slash_Step_0_gml_98_0""@9906
00316: conv.s.v
00317: call.i scr_84_get_lang_string(argc=1)
00319: pushi.e -1
00320: pushi.e 3
00321: pop.v.v [array]line
00323: push.s ""obj_credits_slash_Step_0_gml_99_0""@9907
00325: conv.s.v
00326: call.i scr_84_get_lang_string(argc=1)
00328: pushi.e -1
00329: pushi.e 4
00330: pop.v.v [array]line
00332: push.i 12632256
00334: pushi.e -1
00335: pushi.e 0
00336: pop.v.i [array]linecolor
00338: push.i 16777215
00340: pushi.e -1
00341: pushi.e 1
00342: pop.v.i [array]linecolor
00344: push.i 12632256
00346: pushi.e -1
00347: pushi.e 3
00348: pop.v.i [array]linecolor
00350: push.i 16777215
00352: pushi.e -1
00353: pushi.e 4
00354: pop.v.i [array]linecolor
00356: pushglb.v global.lang
00358: push.s ""ja""@1566
00360: cmp.s.v EQ
00361: bf 00369
00362: push.s ""obj_credits_slash_Step_0_gml_70_0""@9903
00364: conv.s.v
00365: call.i scr_84_get_lang_string(argc=1)
00367: pop.v.v self.lyric
00369: push.v self.timer
00371: pushi.e 480
00372: cmp.i.v GTE
00373: bf 00379
00374: push.v self.timer
00376: pushi.e 520
00377: cmp.i.v LTE
00378: b 00380
00379: push.e 0
00380: bf 00397
00381: push.v self.creditalpha
00383: push.d 0.025
00386: sub.d.v
00387: pop.v.v self.creditalpha
00389: push.v self.textalpha
00391: push.d 0.025
00394: sub.d.v
00395: pop.v.v self.textalpha
00397: push.v self.timer
00399: pushi.e 526
00400: cmp.i.v EQ
00401: bf 00412
00402: pushi.e 1
00403: pop.v.i self.textalpha
00405: push.s ""obj_credits_slash_Step_0_gml_89_0""@9908
00407: conv.s.v
00408: call.i scr_84_get_lang_string(argc=1)
00410: pop.v.v self.lyric
00412: push.v self.timer
00414: pushi.e 573
00415: cmp.i.v EQ
00416: bf 00505
00417: pushi.e 1
00418: pop.v.i self.creditalpha
00420: push.s ""Localization Producers""@9909
00422: pushi.e -1
00423: pushi.e 0
00424: pop.v.s [array]line
00426: push.s ""John Ricciardi""@9910
00428: pushi.e -1
00429: pushi.e 1
00430: pop.v.s [array]line
00432: push.s ""Graeme Howard""@9911
00434: pushi.e -1
00435: pushi.e 2
00436: pop.v.s [array]line
00438: push.i 12632256
00440: pushi.e -1
00441: pushi.e 0
00442: pop.v.i [array]linecolor
00444: push.i 16777215
00446: pushi.e -1
00447: pushi.e 1
00448: pop.v.i [array]linecolor
00450: push.i 16777215
00452: pushi.e -1
00453: pushi.e 2
00454: pop.v.i [array]linecolor
00456: push.i 12632256
00458: pushi.e -1
00459: pushi.e 3
00460: pop.v.i [array]linecolor
00462: push.i 16777215
00464: pushi.e -1
00465: pushi.e 4
00466: pop.v.i [array]linecolor
00468: push.s ""Localization Programming""@9912
00470: pushi.e -1
00471: pushi.e 3
00472: pop.v.s [array]line
00474: push.s ""Gregg Tavares""@9913
00476: pushi.e -1
00477: pushi.e 4
00478: pop.v.s [array]line
00480: pushglb.v global.lang
00482: push.s ""ja""@1566
00484: cmp.s.v EQ
00485: bf 00498
00486: push.s ""ローカライズプロデューサー""@9914
00488: pushi.e -1
00489: pushi.e 0
00490: pop.v.s [array]line
00492: push.s ""ローカライズプログラミング""@9915
00494: pushi.e -1
00495: pushi.e 3
00496: pop.v.s [array]line
00498: push.s ""obj_credits_slash_Step_0_gml_108_0""@9916
00500: conv.s.v
00501: call.i scr_84_get_lang_string(argc=1)
00503: pop.v.v self.lyric
00505: push.v self.timer
00507: pushi.e 645
00508: cmp.i.v EQ
00509: bf 00523
00510: pushglb.v global.lang
00512: push.s ""en""@2775
00514: cmp.s.v EQ
00515: bf 00523
00516: push.s ""obj_credits_slash_Step_0_gml_113_0""@9917
00518: conv.s.v
00519: call.i scr_84_get_lang_string(argc=1)
00521: pop.v.v self.lyric
00523: push.v self.timer
00525: pushi.e 668
00526: cmp.i.v EQ
00527: bf 00622
00528: push.s ""obj_credits_slash_Step_0_gml_119_0""@9918
00530: conv.s.v
00531: call.i scr_84_get_lang_string(argc=1)
00533: pushi.e -1
00534: pushi.e 0
00535: pop.v.v [array]line
00537: push.s ""obj_credits_slash_Step_0_gml_120_0""@9919
00539: conv.s.v
00540: call.i scr_84_get_lang_string(argc=1)
00542: pushi.e -1
00543: pushi.e 1
00544: pop.v.v [array]line
00546: push.s ""obj_credits_slash_Step_0_gml_121_0""@9920
00548: conv.s.v
00549: call.i scr_84_get_lang_string(argc=1)
00551: pushi.e -1
00552: pushi.e 2
00553: pop.v.v [array]line
00555: push.s ""Snowdrake & Monster Kid Design""@9921
00557: pushi.e -1
00558: pushi.e 3
00559: pop.v.s [array]line
00561: push.s ""Magnolia Porter""@9922
00563: pushi.e -1
00564: pushi.e 4
00565: pop.v.s [array]line
00567: push.i 12632256
00569: pushi.e -1
00570: pushi.e 0
00571: pop.v.i [array]linecolor
00573: push.i 12632256
00575: pushi.e -1
00576: pushi.e 1
00577: pop.v.i [array]linecolor
00579: push.i 16777215
00581: pushi.e -1
00582: pushi.e 2
00583: pop.v.i [array]linecolor
00585: push.i 12632256
00587: pushi.e -1
00588: pushi.e 3
00589: pop.v.i [array]linecolor
00591: push.i 16777215
00593: pushi.e -1
00594: pushi.e 4
00595: pop.v.i [array]linecolor
00597: pushglb.v global.lang
00599: push.s ""ja""@1566
00601: cmp.s.v EQ
00602: bf 00609
00603: push.s ""ライちゃん／モンスターの子　デザイン""@9923
00605: pushi.e -1
00606: pushi.e 3
00607: pop.v.s [array]line
00609: pushglb.v global.lang
00611: push.s ""ja""@1566
00613: cmp.s.v EQ
00614: bf 00622
00615: push.s ""obj_credits_slash_Step_0_gml_113_0""@9917
00617: conv.s.v
00618: call.i scr_84_get_lang_string(argc=1)
00620: pop.v.v self.lyric
00622: push.v self.timer
00624: pushi.e 735
00625: cmp.i.v EQ
00626: bf 00634
00627: push.s ""obj_credits_slash_Step_0_gml_131_0""@9924
00629: conv.s.v
00630: call.i scr_84_get_lang_string(argc=1)
00632: pop.v.v self.lyric
00634: push.v self.timer
00636: pushi.e 765
00637: cmp.i.v EQ
00638: bf 00750
00639: push.s ""obj_credits_slash_Step_0_gml_152_0""@9925
00641: conv.s.v
00642: call.i scr_84_get_lang_string(argc=1)
00644: pushi.e -1
00645: pushi.e 0
00646: pop.v.v [array]line
00648: push.s ""Gigi DG (Outfit & Color Assist)""@9926
00650: pushi.e -1
00651: pushi.e 1
00652: pop.v.s [array]line
00654: push.s ""Betty Kwong (Temmie Design)""@9927
00656: pushi.e -1
00657: pushi.e 2
00658: pop.v.s [array]line
00660: push.s ""256graph (JP Graphics)""@9928
00662: pushi.e -1
00663: pushi.e 3
00664: pop.v.s [array]line
00666: push.s ""Ryan Alyea (Website)""@9929
00668: pushi.e -1
00669: pushi.e 4
00670: pop.v.s [array]line
00672: push.s ""Brian Coia (Website)""@9930
00674: pushi.e -1
00675: pushi.e 5
00676: pop.v.s [array]line
00678: push.i 12632256
00680: pushi.e -1
00681: pushi.e 0
00682: pop.v.i [array]linecolor
00684: push.i 16777215
00686: pushi.e -1
00687: pushi.e 1
00688: pop.v.i [array]linecolor
00690: push.i 16777215
00692: pushi.e -1
00693: pushi.e 2
00694: pop.v.i [array]linecolor
00696: push.i 16777215
00698: pushi.e -1
00699: pushi.e 3
00700: pop.v.i [array]linecolor
00702: push.i 16777215
00704: pushi.e -1
00705: pushi.e 4
00706: pop.v.i [array]linecolor
00708: push.i 16777215
00710: pushi.e -1
00711: pushi.e 5
00712: pop.v.i [array]linecolor
00714: pushglb.v global.lang
00716: push.s ""ja""@1566
00718: cmp.s.v EQ
00719: bf 00750
00720: push.s ""Gigi DG (カラーアシタンス)""@9931
00722: pushi.e -1
00723: pushi.e 1
00724: pop.v.s [array]line
00726: push.s ""Betty Kwong (テミー・デザイン)""@9932
00728: pushi.e -1
00729: pushi.e 2
00730: pop.v.s [array]line
00732: push.s ""256graph (日本語グラフィック)""@9933
00734: pushi.e -1
00735: pushi.e 3
00736: pop.v.s [array]line
00738: push.s ""Ryan Alyea (ウェブサイト)""@9934
00740: pushi.e -1
00741: pushi.e 4
00742: pop.v.s [array]line
00744: push.s ""Brian Coia (ウェブサイト)""@9935
00746: pushi.e -1
00747: pushi.e 5
00748: pop.v.s [array]line
00750: push.v self.timer
00752: pushi.e 798
00753: cmp.i.v EQ
00754: bf 00762
00755: push.s ""obj_credits_slash_Step_0_gml_147_0""@9936
00757: conv.s.v
00758: call.i scr_84_get_lang_string(argc=1)
00760: pop.v.v self.lyric
00762: push.v self.timer
00764: pushi.e 870
00765: cmp.i.v EQ
00766: bf 00815
00767: push.s ""obj_credits_slash_Step_0_gml_152_0""@9925
00769: conv.s.v
00770: call.i scr_84_get_lang_string(argc=1)
00772: pushi.e -1
00773: pushi.e 0
00774: pop.v.v [array]line
00776: push.s ""obj_credits_slash_Step_0_gml_153_0""@9937
00778: conv.s.v
00779: call.i scr_84_get_lang_string(argc=1)
00781: pushi.e -1
00782: pushi.e 1
00783: pop.v.v [array]line
00785: push.s ""Fontworks Inc.""@9938
00787: pushi.e -1
00788: pushi.e 2
00789: pop.v.s [array]line
00791: push.s ""Yutaka Sato (Happy Ruika)""@9939
00793: pushi.e -1
00794: pushi.e 3
00795: pop.v.s [array]line
00797: push.s ""Hiroko Minamoto""@9940
00799: pushi.e -1
00800: pushi.e 4
00801: pop.v.s [array]line
00803: push.s ""All 8-4 & Fangamer Staff""@9941
00805: pushi.e -1
00806: pushi.e 5
00807: pop.v.s [array]line
00809: push.i 16777215
00811: pushi.e -1
00812: pushi.e 1
00813: pop.v.i [array]linecolor
00815: push.v self.timer
00817: pushi.e 960
00818: cmp.i.v GTE
00819: bf 00825
00820: push.v self.timer
00822: pushi.e 1030
00823: cmp.i.v LTE
00824: b 00826
00825: push.e 0
00826: bf 00843
00827: push.v self.creditalpha
00829: push.d 0.02
00832: sub.d.v
00833: pop.v.v self.creditalpha
00835: push.v self.textalpha
00837: push.d 0.02
00840: sub.d.v
00841: pop.v.v self.textalpha
00843: push.v self.timer
00845: pushi.e 1033
00846: cmp.i.v EQ
00847: bf 00858
00848: pushi.e 1
00849: pop.v.i self.textalpha
00851: push.s ""obj_credits_slash_Step_0_gml_174_0""@9942
00853: conv.s.v
00854: call.i scr_84_get_lang_string(argc=1)
00856: pop.v.v self.lyric
00858: push.v self.timer
00860: pushi.e 1086
00861: cmp.i.v EQ
00862: bf 00870
00863: push.s ""obj_credits_slash_Step_0_gml_180_0""@9943
00865: conv.s.v
00866: call.i scr_84_get_lang_string(argc=1)
00868: pop.v.v self.lyric
00870: push.v self.timer
00872: pushi.e 1300
00873: cmp.i.v GTE
00874: bf 00977
00875: push.v self.timer
00877: pushi.e 1560
00878: cmp.i.v LTE
00879: bf 00885
00880: push.v self.creditalpha
00882: pushi.e 1
00883: cmp.i.v LT
00884: b 00886
00885: push.e 0
00886: bf 00895
00887: push.v self.creditalpha
00889: push.d 0.01
00892: add.d.v
00893: pop.v.v self.creditalpha
00895: push.v self.timer
00897: pushi.e 1560
00898: cmp.i.v GTE
00899: bf 00905
00900: push.v self.creditalpha
00902: pushi.e 0
00903: cmp.i.v GT
00904: b 00906
00905: push.e 0
00906: bf 00915
00907: push.v self.creditalpha
00909: push.d 0.01
00912: sub.d.v
00913: pop.v.v self.creditalpha
00915: push.s ""obj_credits_slash_Step_0_gml_187_0""@9944
00917: conv.s.v
00918: call.i scr_84_get_lang_string(argc=1)
00920: pushi.e -1
00921: pushi.e 0
00922: pop.v.v [array]line
00924: push.s ""obj_credits_slash_Step_0_gml_188_0""@9945
00926: conv.s.v
00927: call.i scr_84_get_lang_string(argc=1)
00929: pushi.e -1
00930: pushi.e 1
00931: pop.v.v [array]line
00933: push.i 16777215
00935: pushi.e -1
00936: pushi.e 0
00937: pop.v.i [array]linecolor
00939: push.i 16777215
00941: pushi.e -1
00942: pushi.e 1
00943: pop.v.i [array]linecolor
00945: push.s "" ""@24
00947: pushi.e -1
00948: pushi.e 2
00949: pop.v.s [array]line
00951: push.s "" ""@24
00953: pushi.e -1
00954: pushi.e 3
00955: pop.v.s [array]line
00957: push.s "" ""@24
00959: pushi.e -1
00960: pushi.e 4
00961: pop.v.s [array]line
00963: push.s "" ""@24
00965: pushi.e -1
00966: pushi.e 5
00967: pop.v.s [array]line
00969: push.v self.textalpha
00971: push.d 0.01
00974: sub.d.v
00975: pop.v.v self.textalpha
00977: push.v self.timer
00979: pushi.e 1660
00980: cmp.i.v EQ
00981: bf 00987
00982: push.v self.song0
00984: call.i snd_free(argc=1)
00986: popz.v
00987: push.v self.timer
00989: pushi.e 1680
00990: cmp.i.v EQ
00991: bf func_end
00992: call.i ossafe_game_end(argc=0)
00994: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_darkcontroller").EventHandlerFor(EventType.Step, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 0
00001: conv.i.v
00002: pushi.e 0
00003: conv.i.v
00004: call.i __view_get(argc=2)
00006: pop.v.v self.xx
00008: pushi.e 0
00009: conv.i.v
00010: pushi.e 1
00011: conv.i.v
00012: call.i __view_get(argc=2)
00014: pop.v.v self.yy
00016: pushglb.v global.interact
00018: pushi.e 5
00019: cmp.i.v EQ
00020: bf 04980
00021: pushi.e 1
00022: pop.v.i self.charcon
00024: pushglb.v global.submenu
00026: pushi.e 5
00027: cmp.i.v EQ
00028: bt 00034
00029: pushglb.v global.submenu
00031: pushi.e 22
00032: cmp.i.v EQ
00033: b 00035
00034: push.e 1
00035: bf 00341
00036: pushi.e -5
00037: pushglb.v global.submenu
00039: conv.v.i
00040: push.v [array]submenucoord
00042: pop.v.v global.charselect
00044: pushi.e 0
00045: pushi.e -5
00046: pushi.e 0
00047: pop.v.i [array]faceaction
00049: pushi.e 0
00050: pushi.e -5
00051: pushi.e 1
00052: pop.v.i [array]faceaction
00054: pushi.e 0
00055: pushi.e -5
00056: pushi.e 2
00057: pop.v.i [array]faceaction
00059: pushi.e 7
00060: pushi.e -5
00061: pushglb.v global.charselect
00063: conv.v.i
00064: pop.v.i [array]faceaction
00066: call.i left_p(argc=0)
00068: conv.v.b
00069: bf 00101
00070: pushi.e -5
00071: pushglb.v global.submenu
00073: conv.v.i
00074: push.v [array]submenucoord
00076: pushi.e 0
00077: cmp.i.v GT
00078: bf 00091
00079: pushi.e -5
00080: pushglb.v global.submenu
00082: conv.v.i
00083: dup.i 1
00084: push.v [array]submenucoord
00086: pushi.e 1
00087: sub.i.v
00088: pop.i.v [array]submenucoord
00090: b 00101
00091: push.v self.chartotal
00093: pushi.e 1
00094: sub.i.v
00095: pushi.e -5
00096: pushglb.v global.submenu
00098: conv.v.i
00099: pop.v.v [array]submenucoord
00101: call.i right_p(argc=0)
00103: conv.v.b
00104: bf 00136
00105: pushi.e -5
00106: pushglb.v global.submenu
00108: conv.v.i
00109: push.v [array]submenucoord
00111: push.v self.chartotal
00113: pushi.e 1
00114: sub.i.v
00115: cmp.v.v LT
00116: bf 00129
00117: pushi.e -5
00118: pushglb.v global.submenu
00120: conv.v.i
00121: dup.i 1
00122: push.v [array]submenucoord
00124: pushi.e 1
00125: add.i.v
00126: pop.i.v [array]submenucoord
00128: b 00136
00129: pushi.e 0
00130: pushi.e -5
00131: pushglb.v global.submenu
00133: conv.v.i
00134: pop.v.i [array]submenucoord
00136: call.i button1_p(argc=0)
00138: conv.v.b
00139: bf 00145
00140: push.v self.onebuffer
00142: pushi.e 0
00143: cmp.i.v LT
00144: b 00146
00145: push.e 0
00146: bf 00255
00147: pushi.e 2
00148: pop.v.i self.onebuffer
00150: pushglb.v global.submenu
00152: pushi.e 5
00153: cmp.i.v EQ
00154: bf 00196
00155: pushi.e -5
00156: pushi.e -5
00157: pushi.e 2
00158: push.v [array]submenucoord
00160: conv.v.i
00161: push.v [array]item
00163: call.i scr_itemuse(argc=1)
00165: popz.v
00166: push.v self.usable
00168: pushi.e 1
00169: cmp.i.v EQ
00170: bf 00180
00171: pushi.e 0
00172: conv.i.v
00173: pushi.e -5
00174: pushi.e 2
00175: push.v [array]submenucoord
00177: call.i scr_itemshift(argc=2)
00179: popz.v
00180: call.i scr_itemdesc(argc=0)
00182: popz.v
00183: pushi.e 2
00184: pop.v.i global.submenu
00186: pushi.e 0
00187: pushi.e -5
00188: pushglb.v global.charselect
00190: conv.v.i
00191: pop.v.i [array]faceaction
00193: pushi.e -1
00194: pop.v.i global.charselect
00196: pushglb.v global.submenu
00198: pushi.e 22
00199: cmp.i.v EQ
00200: bf 00255
00201: pushi.e -5
00202: pushi.e -5
00203: pushi.e -5
00204: pushi.e 20
00205: push.v [array]submenucoord
00207: conv.v.i
00208: push.v [array]char
00210: conv.v.i
00211: break.e -1
00212: push.i 32000
00214: mul.i.i
00215: pushi.e -5
00216: pushi.e 21
00217: push.v [array]submenucoord
00219: conv.v.i
00220: break.e -1
00221: add.i.i
00222: push.v [array]spell
00224: call.i scr_spell_overworld(argc=1)
00226: popz.v
00227: push.v global.tension
00229: pushi.e -5
00230: pushi.e -5
00231: pushi.e -5
00232: pushi.e 20
00233: push.v [array]submenucoord
00235: conv.v.i
00236: push.v [array]char
00238: conv.v.i
00239: break.e -1
00240: push.i 32000
00242: mul.i.i
00243: pushi.e -5
00244: pushi.e 21
00245: push.v [array]submenucoord
00247: conv.v.i
00248: break.e -1
00249: add.i.i
00250: push.v [array]spellcost
00252: sub.v.v
00253: pop.v.v global.tension
00255: pushi.e 0
00256: pop.v.i self.close
00258: call.i button2_p(argc=0)
00260: conv.v.b
00261: bf 00267
00262: push.v self.twobuffer
00264: pushi.e 0
00265: cmp.i.v LT
00266: b 00268
00267: push.e 0
00268: bf 00272
00269: pushi.e 1
00270: pop.v.i self.close
00272: pushglb.v global.submenu
00274: pushi.e 22
00275: cmp.i.v EQ
00276: bf 00307
00277: pushi.e -5
00278: pushi.e -5
00279: pushi.e -5
00280: pushi.e 20
00281: push.v [array]submenucoord
00283: conv.v.i
00284: push.v [array]char
00286: conv.v.i
00287: break.e -1
00288: push.i 32000
00290: mul.i.i
00291: pushi.e -5
00292: pushi.e 21
00293: push.v [array]submenucoord
00295: conv.v.i
00296: break.e -1
00297: add.i.i
00298: push.v [array]spellcost
00300: pushglb.v global.tension
00302: cmp.v.v GT
00303: bf 00307
00304: pushi.e 1
00305: pop.v.i self.close
00307: push.v self.close
00309: pushi.e 1
00310: cmp.i.v EQ
00311: bf 00341
00312: pushi.e 0
00313: pushi.e -5
00314: pushglb.v global.charselect
00316: conv.v.i
00317: pop.v.i [array]faceaction
00319: pushi.e -1
00320: pop.v.i global.charselect
00322: pushi.e 2
00323: pop.v.i self.twobuffer
00325: pushglb.v global.submenu
00327: pushi.e 5
00328: cmp.i.v EQ
00329: bf 00333
00330: pushi.e 2
00331: pop.v.i global.submenu
00333: pushglb.v global.submenu
00335: pushi.e 22
00336: cmp.i.v EQ
00337: bf 00341
00338: pushi.e 21
00339: pop.v.i global.submenu
00341: pushglb.v global.submenu
00343: pushi.e 6
00344: cmp.i.v EQ
00345: bt 00356
00346: pushglb.v global.submenu
00348: pushi.e 7
00349: cmp.i.v EQ
00350: bt 00356
00351: pushglb.v global.menuno
00353: pushi.e 3
00354: cmp.i.v EQ
00355: b 00357
00356: push.e 1
00357: bf 00825
00358: pushi.e 3
00359: pop.v.i global.charselect
00361: pushi.e 7
00362: pushi.e -5
00363: pushi.e 0
00364: pop.v.i [array]faceaction
00366: pushi.e 7
00367: pushi.e -5
00368: pushi.e 1
00369: pop.v.i [array]faceaction
00371: pushi.e 7
00372: pushi.e -5
00373: pushi.e 2
00374: pop.v.i [array]faceaction
00376: call.i button1_p(argc=0)
00378: conv.v.b
00379: bf 00390
00380: push.v self.onebuffer
00382: pushi.e 0
00383: cmp.i.v LT
00384: bf 00390
00385: pushglb.v global.submenu
00387: pushi.e 6
00388: cmp.i.v EQ
00389: b 00391
00390: push.e 0
00391: bf 00444
00392: pushi.e 2
00393: pop.v.i self.onebuffer
00395: pushi.e 0
00396: pushi.e -5
00397: pushi.e 0
00398: pop.v.i [array]faceaction
00400: pushi.e 0
00401: pushi.e -5
00402: pushi.e 1
00403: pop.v.i [array]faceaction
00405: pushi.e 0
00406: pushi.e -5
00407: pushi.e 2
00408: pop.v.i [array]faceaction
00410: pushi.e -5
00411: pushi.e -5
00412: pushi.e 2
00413: push.v [array]submenucoord
00415: conv.v.i
00416: push.v [array]item
00418: call.i scr_itemuse(argc=1)
00420: popz.v
00421: push.v self.usable
00423: pushi.e 1
00424: cmp.i.v EQ
00425: bf 00435
00426: pushi.e 0
00427: conv.i.v
00428: pushi.e -5
00429: pushi.e 2
00430: push.v [array]submenucoord
00432: call.i scr_itemshift(argc=2)
00434: popz.v
00435: call.i scr_itemdesc(argc=0)
00437: popz.v
00438: pushi.e -1
00439: pop.v.i global.charselect
00441: pushi.e 2
00442: pop.v.i global.submenu
00444: call.i button1_p(argc=0)
00446: conv.v.b
00447: bf 00458
00448: push.v self.onebuffer
00450: pushi.e 0
00451: cmp.i.v LT
00452: bf 00458
00453: pushglb.v global.submenu
00455: pushi.e 7
00456: cmp.i.v EQ
00457: b 00459
00458: push.e 0
00459: bf 00717
00460: pushi.e 2
00461: pop.v.i self.onebuffer
00463: pushi.e 0
00464: pushi.e -5
00465: pushi.e 0
00466: pop.v.i [array]faceaction
00468: pushi.e 0
00469: pushi.e -5
00470: pushi.e 1
00471: pop.v.i [array]faceaction
00473: pushi.e 0
00474: pushi.e -5
00475: pushi.e 2
00476: pop.v.i [array]faceaction
00478: pushi.e -5
00479: pushi.e -5
00480: pushi.e 2
00481: push.v [array]submenucoord
00483: conv.v.i
00484: push.v [array]item
00486: pop.v.v self.throwitem
00488: pushi.e 0
00489: conv.i.v
00490: pushi.e -5
00491: pushi.e 2
00492: push.v [array]submenucoord
00494: call.i scr_itemshift(argc=2)
00496: popz.v
00497: call.i scr_itemdesc(argc=0)
00499: popz.v
00500: pushi.e -1
00501: pop.v.i global.charselect
00503: pushi.e 3
00504: pop.v.i global.submenu
00506: push.v self.throwitem
00508: pushi.e 4
00509: cmp.i.v EQ
00510: bf 00717
00511: pushi.e -5
00512: pushi.e 2
00513: push.v [array]char
00515: pushi.e 3
00516: cmp.i.v EQ
00517: bt 00525
00518: pushi.e -5
00519: pushi.e 1
00520: push.v [array]char
00522: pushi.e 3
00523: cmp.i.v EQ
00524: b 00526
00525: push.e 1
00526: bf 00717
00527: pushi.e 1
00528: pop.v.i global.interact
00530: call.i scr_closemenu(argc=0)
00532: popz.v
00533: pushi.e 2
00534: pop.v.i global.fc
00536: pushi.e 31
00537: pop.v.i global.typer
00539: pushi.e 9
00540: pop.v.i global.fe
00542: push.s ""obj_darkcontroller_slash_Step_0_gml_129_0""@6995
00544: conv.s.v
00545: call.i scr_84_get_lang_string(argc=1)
00547: pushi.e -5
00548: pushi.e 0
00549: pop.v.v [array]msg
00551: pushi.e -5
00552: pushi.e 207
00553: push.v [array]flag
00555: pushi.e 1
00556: cmp.i.v EQ
00557: bf 00612
00558: pushi.e 0
00559: pop.v.i global.fc
00561: pushi.e 6
00562: pop.v.i global.typer
00564: push.s ""obj_darkcontroller_slash_Step_0_gml_135_0""@6996
00566: conv.s.v
00567: call.i scr_84_get_lang_string(argc=1)
00569: pushi.e -5
00570: pushi.e 0
00571: pop.v.v [array]msg
00573: pushi.e 9
00574: conv.i.v
00575: pushi.e 1
00576: conv.i.v
00577: call.i scr_ralface(argc=2)
00579: popz.v
00580: push.s ""obj_darkcontroller_slash_Step_0_gml_137_0""@6997
00582: conv.s.v
00583: call.i scr_84_get_lang_string(argc=1)
00585: pushi.e -5
00586: pushi.e 2
00587: pop.v.v [array]msg
00589: push.s ""obj_darkcontroller_slash_Step_0_gml_138_0""@6998
00591: conv.s.v
00592: call.i scr_84_get_lang_string(argc=1)
00594: pushi.e -5
00595: pushi.e 3
00596: pop.v.v [array]msg
00598: push.s ""obj_darkcontroller_slash_Step_0_gml_139_0""@6999
00600: conv.s.v
00601: call.i scr_84_get_lang_string(argc=1)
00603: pushi.e -5
00604: pushi.e 4
00605: pop.v.v [array]msg
00607: pushi.e 2
00608: pushi.e -5
00609: pushi.e 207
00610: pop.v.i [array]flag
00612: pushi.e -5
00613: pushi.e 207
00614: push.v [array]flag
00616: pushi.e 0
00617: cmp.i.v EQ
00618: bf 00701
00619: pushi.e 0
00620: pop.v.i global.fc
00622: pushi.e 6
00623: pop.v.i global.typer
00625: push.s ""obj_darkcontroller_slash_Step_0_gml_147_0""@7000
00627: conv.s.v
00628: call.i scr_84_get_lang_string(argc=1)
00630: pushi.e -5
00631: pushi.e 0
00632: pop.v.v [array]msg
00634: pushi.e 0
00635: conv.i.v
00636: pushi.e 1
00637: conv.i.v
00638: call.i scr_ralface(argc=2)
00640: popz.v
00641: push.s ""obj_darkcontroller_slash_Step_0_gml_149_0""@7001
00643: conv.s.v
00644: call.i scr_84_get_lang_string(argc=1)
00646: pushi.e -5
00647: pushi.e 2
00648: pop.v.v [array]msg
00650: push.s ""obj_darkcontroller_slash_Step_0_gml_150_0""@7002
00652: conv.s.v
00653: call.i scr_84_get_lang_string(argc=1)
00655: pushi.e -5
00656: pushi.e 3
00657: pop.v.v [array]msg
00659: push.s ""obj_darkcontroller_slash_Step_0_gml_151_0""@7003
00661: conv.s.v
00662: call.i scr_84_get_lang_string(argc=1)
00664: pushi.e -5
00665: pushi.e 4
00666: pop.v.v [array]msg
00668: push.s ""obj_darkcontroller_slash_Step_0_gml_152_0""@7004
00670: conv.s.v
00671: call.i scr_84_get_lang_string(argc=1)
00673: pushi.e -5
00674: pushi.e 5
00675: pop.v.v [array]msg
00677: pushi.e 6
00678: conv.i.v
00679: call.i scr_noface(argc=1)
00681: popz.v
00682: push.s ""obj_darkcontroller_slash_Step_0_gml_154_0""@7005
00684: conv.s.v
00685: call.i scr_84_get_lang_string(argc=1)
00687: pushi.e -5
00688: pushi.e 7
00689: pop.v.v [array]msg
00691: pushi.e 4
00692: conv.i.v
00693: call.i scr_itemget(argc=1)
00695: popz.v
00696: pushi.e 1
00697: pushi.e -5
00698: pushi.e 207
00699: pop.v.i [array]flag
00701: pushi.e 5
00702: conv.i.v
00703: pushi.e 0
00704: conv.i.v
00705: pushi.e 0
00706: conv.i.v
00707: call.i instance_create(argc=3)
00709: pop.v.v self.dl
00711: pushi.e 1
00712: push.v self.dl
00714: conv.v.i
00715: pop.v.i [stacktop]free
00717: call.i button1_p(argc=0)
00719: conv.v.b
00720: bf 00731
00721: push.v self.onebuffer
00723: pushi.e 0
00724: cmp.i.v LT
00725: bf 00731
00726: pushglb.v global.menuno
00728: pushi.e 3
00729: cmp.i.v EQ
00730: b 00732
00731: push.e 0
00732: bf 00769
00733: pushi.e 2
00734: pop.v.i self.twobuffer
00736: pushi.e 2
00737: pop.v.i self.onebuffer
00739: pushi.e 0
00740: pushi.e -5
00741: pushi.e 0
00742: pop.v.i [array]faceaction
00744: pushi.e 0
00745: pushi.e -5
00746: pushi.e 1
00747: pop.v.i [array]faceaction
00749: pushi.e 0
00750: pushi.e -5
00751: pushi.e 2
00752: pop.v.i [array]faceaction
00754: pushi.e -1
00755: pop.v.i global.charselect
00757: pushi.e 6
00758: pop.v.i global.interact
00760: call.i scr_talkroom(argc=0)
00762: popz.v
00763: pushi.e -1
00764: pop.v.i global.menuno
00766: pushi.e 0
00767: pop.v.i self.charcon
00769: call.i button2_p(argc=0)
00771: conv.v.b
00772: bf 00778
00773: push.v self.twobuffer
00775: pushi.e 0
00776: cmp.i.v LT
00777: b 00779
00778: push.e 0
00779: bf 00825
00780: pushi.e 2
00781: pop.v.i self.twobuffer
00783: pushi.e 0
00784: pushi.e -5
00785: pushi.e 0
00786: pop.v.i [array]faceaction
00788: pushi.e 0
00789: pushi.e -5
00790: pushi.e 1
00791: pop.v.i [array]faceaction
00793: pushi.e 0
00794: pushi.e -5
00795: pushi.e 2
00796: pop.v.i [array]faceaction
00798: pushglb.v global.submenu
00800: pushi.e 6
00801: cmp.i.v EQ
00802: bf 00806
00803: pushi.e 2
00804: pop.v.i global.submenu
00806: pushglb.v global.submenu
00808: pushi.e 7
00809: cmp.i.v EQ
00810: bf 00814
00811: pushi.e 3
00812: pop.v.i global.submenu
00814: pushglb.v global.menuno
00816: pushi.e 3
00817: cmp.i.v EQ
00818: bf 00822
00819: pushi.e 0
00820: pop.v.i global.menuno
00822: pushi.e -1
00823: pop.v.i global.charselect
00825: pushglb.v global.menuno
00827: pushi.e 5
00828: cmp.i.v EQ
00829: bf 02332
00830: pushglb.v global.submenu
00832: pushi.e 30
00833: cmp.i.v EQ
00834: bf 01062
00835: pushi.e 0
00836: pop.v.i self.sndbuffer
00838: pushi.e 0
00839: pop.v.i self.m_quit
00841: call.i up_p(argc=0)
00843: conv.v.b
00844: bf 00866
00845: pushi.e -5
00846: pushi.e 30
00847: dup.i 1
00848: push.v [array]submenucoord
00850: pushi.e 1
00851: sub.i.v
00852: pop.i.v [array]submenucoord
00854: pushi.e -5
00855: pushi.e 30
00856: push.v [array]submenucoord
00858: pushi.e 0
00859: cmp.i.v LT
00860: bf 00866
00861: pushi.e 0
00862: pushi.e -5
00863: pushi.e 30
00864: pop.v.i [array]submenucoord
00866: call.i down_p(argc=0)
00868: conv.v.b
00869: bf 00891
00870: pushi.e -5
00871: pushi.e 30
00872: dup.i 1
00873: push.v [array]submenucoord
00875: pushi.e 1
00876: add.i.v
00877: pop.i.v [array]submenucoord
00879: pushi.e -5
00880: pushi.e 30
00881: push.v [array]submenucoord
00883: pushi.e 6
00884: cmp.i.v GT
00885: bf 00891
00886: pushi.e 6
00887: pushi.e -5
00888: pushi.e 30
00889: pop.v.i [array]submenucoord
00891: call.i button1_p(argc=0)
00893: conv.v.b
00894: bf 00900
00895: push.v self.onebuffer
00897: pushi.e 0
00898: cmp.i.v LT
00899: b 00901
00900: push.e 0
00901: bf 01031
00902: pushi.e 2
00903: pop.v.i self.upbuffer
00905: pushi.e 2
00906: pop.v.i self.downbuffer
00908: pushi.e 2
00909: pop.v.i self.onebuffer
00911: pushi.e 2
00912: pop.v.i self.twobuffer
00914: pushi.e 1
00915: pop.v.i self.selectnoise
00917: pushi.e -5
00918: pushi.e 30
00919: push.v [array]submenucoord
00921: pushi.e 0
00922: cmp.i.v EQ
00923: bf 00927
00924: pushi.e 33
00925: pop.v.i global.submenu
00927: pushi.e -5
00928: pushi.e 30
00929: push.v [array]submenucoord
00931: pushi.e 1
00932: cmp.i.v EQ
00933: bf 00948
00934: pushi.e 35
00935: pop.v.i global.submenu
00937: pushi.e 0
00938: pushi.e -5
00939: pushi.e 35
00940: pop.v.i [array]submenucoord
00942: pushi.e 0
00943: pop.v.i self.control_select_con
00945: pushi.e 0
00946: pop.v.i self.control_flash_timer
00948: pushi.e -5
00949: pushi.e 30
00950: push.v [array]submenucoord
00952: pushi.e 2
00953: cmp.i.v EQ
00954: bf 00973
00955: pushi.e -5
00956: pushi.e 8
00957: push.v [array]flag
00959: pushi.e 0
00960: cmp.i.v EQ
00961: bf 00968
00962: pushi.e 1
00963: pushi.e -5
00964: pushi.e 8
00965: pop.v.i [array]flag
00967: b 00973
00968: pushi.e 0
00969: pushi.e -5
00970: pushi.e 8
00971: pop.v.i [array]flag
00973: pushi.e -5
00974: pushi.e 30
00975: push.v [array]submenucoord
00977: pushi.e 3
00978: cmp.i.v EQ
00979: bf 00986
00980: pushi.e 320
00981: pushenv 00985
00982: pushi.e 1
00983: pop.v.i self.fullscreen_toggle
00985: popenv 00982
00986: pushi.e -5
00987: pushi.e 30
00988: push.v [array]submenucoord
00990: pushi.e 4
00991: cmp.i.v EQ
00992: bf 01011
00993: pushi.e -5
00994: pushi.e 11
00995: push.v [array]flag
00997: pushi.e 0
00998: cmp.i.v EQ
00999: bf 01006
01000: pushi.e 1
01001: pushi.e -5
01002: pushi.e 11
01003: pop.v.i [array]flag
01005: b 01011
01006: pushi.e 0
01007: pushi.e -5
01008: pushi.e 11
01009: pop.v.i [array]flag
01011: pushi.e -5
01012: pushi.e 30
01013: push.v [array]submenucoord
01015: pushi.e 5
01016: cmp.i.v EQ
01017: bf 01021
01018: pushi.e 34
01019: pop.v.i global.submenu
01021: pushi.e -5
01022: pushi.e 30
01023: push.v [array]submenucoord
01025: pushi.e 6
01026: cmp.i.v EQ
01027: bf 01031
01028: pushi.e 1
01029: pop.v.i self.m_quit
01031: call.i button2_p(argc=0)
01033: conv.v.b
01034: bf 01040
01035: push.v self.twobuffer
01037: pushi.e 0
01038: cmp.i.v LT
01039: b 01041
01040: push.e 0
01041: bf 01045
01042: pushi.e 1
01043: pop.v.i self.m_quit
01045: push.v self.m_quit
01047: pushi.e 1
01048: cmp.i.v EQ
01049: bf 01062
01050: pushi.e 2
01051: pop.v.i self.onebuffer
01053: pushi.e 2
01054: pop.v.i self.twobuffer
01056: pushi.e 0
01057: pop.v.i global.menuno
01059: pushi.e 0
01060: pop.v.i global.submenu
01062: pushglb.v global.submenu
01064: pushi.e 31
01065: cmp.i.v EQ
01066: bt 01077
01067: pushglb.v global.submenu
01069: pushi.e 32
01070: cmp.i.v EQ
01071: bt 01077
01072: pushglb.v global.submenu
01074: pushi.e 33
01075: cmp.i.v EQ
01076: b 01078
01077: push.e 1
01078: bf 01397
01079: pushi.e 0
01080: pop.v.i self.se_select
01082: push.v self.sndbuffer
01084: pushi.e 1
01085: sub.i.v
01086: pop.v.v self.sndbuffer
01088: pushi.e 0
01089: pop.v.i self.muschange
01091: pushi.e 0
01092: pop.v.i self.sndchange
01094: pushi.e 0
01095: pop.v.i self.audchange
01097: call.i right_h(argc=0)
01099: conv.v.b
01100: bf 01179
01101: pushglb.v global.submenu
01103: pushi.e 31
01104: cmp.i.v EQ
01105: bf 01127
01106: pushi.e 1
01107: pop.v.i self.sndchange
01109: pushi.e -5
01110: pushi.e 15
01111: push.v [array]flag
01113: pushi.e 1
01114: cmp.i.v LT
01115: bf 01127
01116: pushi.e -5
01117: pushi.e 15
01118: dup.i 1
01119: push.v [array]flag
01121: push.d 0.05
01124: add.d.v
01125: pop.i.v [array]flag
01127: pushglb.v global.submenu
01129: pushi.e 32
01130: cmp.i.v EQ
01131: bf 01153
01132: pushi.e 1
01133: pop.v.i self.muschange
01135: pushi.e -5
01136: pushi.e 16
01137: push.v [array]flag
01139: pushi.e 1
01140: cmp.i.v LT
01141: bf 01153
01142: pushi.e -5
01143: pushi.e 16
01144: dup.i 1
01145: push.v [array]flag
01147: push.d 0.05
01150: add.d.v
01151: pop.i.v [array]flag
01153: pushglb.v global.submenu
01155: pushi.e 33
01156: cmp.i.v EQ
01157: bf 01179
01158: pushi.e -5
01159: pushi.e 17
01160: push.v [array]flag
01162: pushi.e 1
01163: cmp.i.v LT
01164: bf 01176
01165: pushi.e -5
01166: pushi.e 17
01167: dup.i 1
01168: push.v [array]flag
01170: push.d 0.02
01173: add.d.v
01174: pop.i.v [array]flag
01176: pushi.e 1
01177: pop.v.i self.audchange
01179: call.i left_h(argc=0)
01181: conv.v.b
01182: bf 01263
01183: pushglb.v global.submenu
01185: pushi.e 31
01186: cmp.i.v EQ
01187: bf 01209
01188: pushi.e 1
01189: pop.v.i self.sndchange
01191: pushi.e -5
01192: pushi.e 15
01193: push.v [array]flag
01195: pushi.e 0
01196: cmp.i.v GT
01197: bf 01209
01198: pushi.e -5
01199: pushi.e 15
01200: dup.i 1
01201: push.v [array]flag
01203: push.d 0.05
01206: sub.d.v
01207: pop.i.v [array]flag
01209: pushglb.v global.submenu
01211: pushi.e 32
01212: cmp.i.v EQ
01213: bf 01235
01214: pushi.e 1
01215: pop.v.i self.muschange
01217: pushi.e -5
01218: pushi.e 16
01219: push.v [array]flag
01221: pushi.e 0
01222: cmp.i.v GT
01223: bf 01235
01224: pushi.e -5
01225: pushi.e 16
01226: dup.i 1
01227: push.v [array]flag
01229: push.d 0.05
01232: sub.d.v
01233: pop.i.v [array]flag
01235: pushglb.v global.submenu
01237: pushi.e 33
01238: cmp.i.v EQ
01239: bf 01263
01240: pushi.e 1
01241: pop.v.i self.audchange
01243: pushi.e -5
01244: pushi.e 17
01245: push.v [array]flag
01247: push.d 0.02
01250: cmp.d.v GTE
01251: bf 01263
01252: pushi.e -5
01253: pushi.e 17
01254: dup.i 1
01255: push.v [array]flag
01257: push.d 0.02
01260: sub.d.v
01261: pop.i.v [array]flag
01263: push.v self.sndchange
01265: pushi.e 1
01266: cmp.i.v EQ
01267: bf 01273
01268: push.v self.sndbuffer
01270: pushi.e 0
01271: cmp.i.v LT
01272: b 01274
01273: push.e 0
01274: bf 01294
01275: pushi.e 0
01276: conv.i.v
01277: pushi.e -5
01278: pushi.e 15
01279: push.v [array]flag
01281: pushi.e 1
01282: conv.i.v
01283: call.i audio_group_set_gain(argc=3)
01285: popz.v
01286: pushi.e 38
01287: conv.i.v
01288: call.i snd_play(argc=1)
01290: popz.v
01291: pushi.e 2
01292: pop.v.i self.sndbuffer
01294: push.v self.muschange
01296: pushi.e 1
01297: cmp.i.v EQ
01298: bf 01323
01299: pushi.e -5
01300: pushi.e 1
01301: push.v [array]currentsong
01303: call.i snd_is_playing(argc=1)
01305: conv.v.b
01306: bf 01323
01307: pushi.e 0
01308: conv.i.v
01309: push.v self.getmusvol
01311: pushi.e -5
01312: pushi.e 16
01313: push.v [array]flag
01315: mul.v.v
01316: pushi.e -5
01317: pushi.e 1
01318: push.v [array]currentsong
01320: call.i mus_volume(argc=3)
01322: popz.v
01323: push.v self.audchange
01325: pushi.e 1
01326: cmp.i.v EQ
01327: bf 01333
01328: push.v self.sndbuffer
01330: pushi.e 0
01331: cmp.i.v LT
01332: b 01334
01333: push.e 0
01334: bf 01352
01335: pushi.e 38
01336: conv.i.v
01337: call.i snd_play(argc=1)
01339: popz.v
01340: pushi.e 2
01341: pop.v.i self.sndbuffer
01343: pushi.e -5
01344: pushi.e 17
01345: push.v [array]flag
01347: pushi.e 0
01348: conv.i.v
01349: call.i audio_set_master_gain(argc=2)
01351: popz.v
01352: call.i button1_p(argc=0)
01354: conv.v.b
01355: bf 01361
01356: push.v self.onebuffer
01358: pushi.e 0
01359: cmp.i.v LT
01360: b 01362
01361: push.e 0
01362: bf 01366
01363: pushi.e 1
01364: pop.v.i self.se_select
01366: call.i button2_p(argc=0)
01368: conv.v.b
01369: bf 01375
01370: push.v self.twobuffer
01372: pushi.e 0
01373: cmp.i.v LT
01374: b 01376
01375: push.e 0
01376: bf 01380
01377: pushi.e 1
01378: pop.v.i self.se_select
01380: push.v self.se_select
01382: pushi.e 1
01383: cmp.i.v EQ
01384: bf 01397
01385: pushi.e 1
01386: pop.v.i self.selectnoise
01388: pushi.e 2
01389: pop.v.i self.onebuffer
01391: pushi.e 2
01392: pop.v.i self.twobuffer
01394: pushi.e 30
01395: pop.v.i global.submenu
01397: pushglb.v global.submenu
01399: pushi.e 34
01400: cmp.i.v EQ
01401: bf 01431
01402: pushi.e 27
01403: conv.i.v
01404: call.i keyboard_check_pressed(argc=1)
01406: conv.v.b
01407: bf 01411
01408: call.i ossafe_game_end(argc=0)
01410: popz.v
01411: call.i button2_p(argc=0)
01413: conv.v.b
01414: bf 01420
01415: push.v self.twobuffer
01417: pushi.e 0
01418: cmp.i.v LT
01419: b 01421
01420: push.e 0
01421: bf 01431
01422: pushi.e 2
01423: pop.v.i self.onebuffer
01425: pushi.e 2
01426: pop.v.i self.twobuffer
01428: pushi.e 30
01429: pop.v.i global.submenu
01431: pushglb.v global.submenu
01433: pushi.e 35
01434: cmp.i.v EQ
01435: bf 02332
01436: pushi.e 0
01437: pop.v.i self.control_select_timer
01439: push.v self.control_flash_timer
01441: pushi.e 1
01442: sub.i.v
01443: pop.v.v self.control_flash_timer
01445: pushi.e 0
01446: pop.v.i self.controls_quitmenu
01448: push.v 320.gamepad_active
01450: pop.v.v self.gamepad_exists
01452: pushi.e 0
01453: pop.v.i self.gamepad_id
01455: push.v self.control_select_con
01457: pushi.e 1
01458: cmp.i.v EQ
01459: bf 01803
01460: pushi.e -1
01461: pop.v.i self.gamepad_accept
01463: pushi.e -1
01464: pop.v.i self.new_gamepad_key
01466: pushi.e -1
01467: pop.v.i self.key_accept
01469: pushi.e -1
01470: pop.v.i self.new_key
01472: pushi.e 1
01473: conv.i.v
01474: call.i keyboard_check_pressed(argc=1)
01476: conv.v.b
01477: bf 01746
01478: pushi.e 48
01479: pop.v.i self.i
01481: push.v self.i
01483: pushi.e 90
01484: cmp.i.v LTE
01485: bf 01506
01486: push.v self.i
01488: call.i keyboard_check_pressed(argc=1)
01490: conv.v.b
01491: bf 01499
01492: push.v self.i
01494: pop.v.v self.new_key
01496: pushi.e 2
01497: pop.v.i self.control_select_con
01499: push.v self.i
01501: pushi.e 1
01502: add.i.v
01503: pop.v.v self.i
01505: b 01481
01506: pushi.e 59
01507: conv.i.v
01508: call.i keyboard_check_pressed(argc=1)
01510: conv.v.b
01511: bf 01518
01512: pushi.e 59
01513: pop.v.i self.new_key
01515: pushi.e 2
01516: pop.v.i self.control_select_con
01518: pushi.e 44
01519: conv.i.v
01520: call.i keyboard_check_pressed(argc=1)
01522: conv.v.b
01523: bf 01530
01524: pushi.e 44
01525: pop.v.i self.new_key
01527: pushi.e 2
01528: pop.v.i self.control_select_con
01530: pushi.e 46
01531: conv.i.v
01532: call.i keyboard_check_pressed(argc=1)
01534: conv.v.b
01535: bf 01542
01536: pushi.e 46
01537: pop.v.i self.new_key
01539: pushi.e 2
01540: pop.v.i self.control_select_con
01542: pushi.e 47
01543: conv.i.v
01544: call.i keyboard_check_pressed(argc=1)
01546: conv.v.b
01547: bf 01554
01548: pushi.e 47
01549: pop.v.i self.new_key
01551: pushi.e 2
01552: pop.v.i self.control_select_con
01554: pushi.e 92
01555: conv.i.v
01556: call.i keyboard_check_pressed(argc=1)
01558: conv.v.b
01559: bf 01566
01560: pushi.e 92
01561: pop.v.i self.new_key
01563: pushi.e 2
01564: pop.v.i self.control_select_con
01566: pushi.e 93
01567: conv.i.v
01568: call.i keyboard_check_pressed(argc=1)
01570: conv.v.b
01571: bf 01578
01572: pushi.e 93
01573: pop.v.i self.new_key
01575: pushi.e 2
01576: pop.v.i self.control_select_con
01578: pushi.e 91
01579: conv.i.v
01580: call.i keyboard_check_pressed(argc=1)
01582: conv.v.b
01583: bf 01590
01584: pushi.e 91
01585: pop.v.i self.new_key
01587: pushi.e 2
01588: pop.v.i self.control_select_con
01590: pushi.e 96
01591: conv.i.v
01592: call.i keyboard_check_pressed(argc=1)
01594: conv.v.b
01595: bf 01602
01596: pushi.e 96
01597: pop.v.i self.new_key
01599: pushi.e 2
01600: pop.v.i self.control_select_con
01602: pushi.e 45
01603: conv.i.v
01604: call.i keyboard_check_pressed(argc=1)
01606: conv.v.b
01607: bf 01614
01608: pushi.e 45
01609: pop.v.i self.new_key
01611: pushi.e 2
01612: pop.v.i self.control_select_con
01614: pushi.e 61
01615: conv.i.v
01616: call.i keyboard_check_pressed(argc=1)
01618: conv.v.b
01619: bf 01626
01620: pushi.e 61
01621: pop.v.i self.new_key
01623: pushi.e 2
01624: pop.v.i self.control_select_con
01626: pushi.e 37
01627: conv.i.v
01628: call.i keyboard_check_pressed(argc=1)
01630: conv.v.b
01631: bf 01638
01632: pushi.e 37
01633: pop.v.i self.new_key
01635: pushi.e 2
01636: pop.v.i self.control_select_con
01638: pushi.e 39
01639: conv.i.v
01640: call.i keyboard_check_pressed(argc=1)
01642: conv.v.b
01643: bf 01650
01644: pushi.e 39
01645: pop.v.i self.new_key
01647: pushi.e 2
01648: pop.v.i self.control_select_con
01650: pushi.e 38
01651: conv.i.v
01652: call.i keyboard_check_pressed(argc=1)
01654: conv.v.b
01655: bf 01662
01656: pushi.e 38
01657: pop.v.i self.new_key
01659: pushi.e 2
01660: pop.v.i self.control_select_con
01662: pushi.e 40
01663: conv.i.v
01664: call.i keyboard_check_pressed(argc=1)
01666: conv.v.b
01667: bf 01674
01668: pushi.e 40
01669: pop.v.i self.new_key
01671: pushi.e 2
01672: pop.v.i self.control_select_con
01674: pushi.e 13
01675: conv.i.v
01676: call.i keyboard_check_pressed(argc=1)
01678: conv.v.b
01679: bf 01686
01680: pushi.e 13
01681: pop.v.i self.new_key
01683: pushi.e 2
01684: pop.v.i self.control_select_con
01686: pushi.e 16
01687: conv.i.v
01688: call.i keyboard_check_pressed(argc=1)
01690: conv.v.b
01691: bf 01698
01692: pushi.e 16
01693: pop.v.i self.new_key
01695: pushi.e 2
01696: pop.v.i self.control_select_con
01698: pushi.e 17
01699: conv.i.v
01700: call.i keyboard_check_pressed(argc=1)
01702: conv.v.b
01703: bf 01710
01704: pushi.e 17
01705: pop.v.i self.new_key
01707: pushi.e 2
01708: pop.v.i self.control_select_con
01710: pushi.e 8
01711: conv.i.v
01712: call.i keyboard_check_pressed(argc=1)
01714: conv.v.b
01715: bf 01722
01716: pushi.e 8
01717: pop.v.i self.new_key
01719: pushi.e 2
01720: pop.v.i self.control_select_con
01722: pushi.e 18
01723: conv.i.v
01724: call.i keyboard_check_pressed(argc=1)
01726: conv.v.b
01727: bf 01734
01728: pushi.e 18
01729: pop.v.i self.new_key
01731: pushi.e 2
01732: pop.v.i self.control_select_con
01734: pushi.e 27
01735: conv.i.v
01736: call.i keyboard_check_pressed(argc=1)
01738: conv.v.b
01739: bf 01746
01740: pushi.e -1
01741: pop.v.i self.new_key
01743: pushi.e 0
01744: pop.v.i self.control_select_con
01746: push.v self.gamepad_exists
01748: pushi.e 1
01749: cmp.i.v EQ
01750: bf 01756
01751: push.v self.control_select_con
01753: pushi.e 1
01754: cmp.i.v EQ
01755: b 01757
01756: push.e 0
01757: bf 01803
01758: push.v self.gamepad_id
01760: call.i gamepad_button_count(argc=1)
01762: pop.v.v self.gpc
01764: push.v self.gpc
01766: pushi.e 40
01767: cmp.i.v GTE
01768: bf 01772
01769: pushi.e 40
01770: pop.v.i self.gpc
01772: pushi.e 0
01773: pop.v.i self.i
01775: push.v self.i
01777: push.v self.gpc
01779: cmp.v.v LTE
01780: bf 01803
01781: push.v self.i
01783: push.v self.gamepad_id
01785: call.i gamepad_button_check_pressed(argc=2)
01787: conv.v.b
01788: bf 01796
01789: push.v self.i
01791: pop.v.v self.new_gamepad_key
01793: pushi.e 2
01794: pop.v.i self.control_select_con
01796: push.v self.i
01798: pushi.e 1
01799: add.i.v
01800: pop.v.v self.i
01802: b 01775
01803: call.i button1_p(argc=0)
01805: conv.v.b
01806: bf 01817
01807: push.v self.control_select_con
01809: pushi.e 0
01810: cmp.i.v EQ
01811: bf 01817
01812: push.v self.onebuffer
01814: pushi.e 0
01815: cmp.i.v LT
01816: b 01818
01817: push.e 0
01818: bf 01866
01819: pushi.e 2
01820: pop.v.i self.onebuffer
01822: pushi.e -5
01823: pushi.e 35
01824: push.v [array]submenucoord
01826: pushi.e 7
01827: cmp.i.v LT
01828: bf 01838
01829: pushi.e 1
01830: pop.v.i self.control_select_con
01832: pushi.e -1
01833: pop.v.i self.keyboard_lastkey
01835: pushi.e 1
01836: pop.v.i self.selectnoise
01838: pushi.e -5
01839: pushi.e 35
01840: push.v [array]submenucoord
01842: pushi.e 7
01843: cmp.i.v EQ
01844: bf 01856
01845: pushi.e 100
01846: conv.i.v
01847: call.i snd_play(argc=1)
01849: popz.v
01850: call.i scr_controls_default(argc=0)
01852: popz.v
01853: pushi.e 10
01854: pop.v.i self.control_flash_timer
01856: pushi.e -5
01857: pushi.e 35
01858: push.v [array]submenucoord
01860: pushi.e 8
01861: cmp.i.v EQ
01862: bf 01866
01863: pushi.e 1
01864: pop.v.i self.controls_quitmenu
01866: push.v self.control_select_con
01868: pushi.e 0
01869: cmp.i.v EQ
01870: bf 01973
01871: call.i down_p(argc=0)
01873: conv.v.b
01874: bf 01885
01875: push.v self.controls_quitmenu
01877: pushi.e 0
01878: cmp.i.v EQ
01879: bf 01885
01880: push.v self.downbuffer
01882: pushi.e 0
01883: cmp.i.v LT
01884: b 01886
01885: push.e 0
01886: bf 01906
01887: pushi.e -5
01888: pushi.e 35
01889: push.v [array]submenucoord
01891: pushi.e 8
01892: cmp.i.v LT
01893: bf 01906
01894: pushi.e -5
01895: pushi.e 35
01896: dup.i 1
01897: push.v [array]submenucoord
01899: pushi.e 1
01900: add.i.v
01901: pop.i.v [array]submenucoord
01903: pushi.e 1
01904: pop.v.i self.movenoise
01906: call.i up_p(argc=0)
01908: conv.v.b
01909: bf 01920
01910: push.v self.controls_quitmenu
01912: pushi.e 0
01913: cmp.i.v EQ
01914: bf 01920
01915: push.v self.upbuffer
01917: pushi.e 0
01918: cmp.i.v LT
01919: b 01921
01920: push.e 0
01921: bf 01941
01922: pushi.e -5
01923: pushi.e 35
01924: push.v [array]submenucoord
01926: pushi.e 0
01927: cmp.i.v GT
01928: bf 01941
01929: pushi.e -5
01930: pushi.e 35
01931: dup.i 1
01932: push.v [array]submenucoord
01934: pushi.e 1
01935: sub.i.v
01936: pop.i.v [array]submenucoord
01938: pushi.e 1
01939: pop.v.i self.movenoise
01941: call.i button1_p(argc=0)
01943: conv.v.b
01944: bf 01955
01945: push.v self.controls_quitmenu
01947: pushi.e 0
01948: cmp.i.v EQ
01949: bf 01955
01950: push.v self.onebuffer
01952: pushi.e 2
01953: cmp.i.v LT
01954: b 01956
01955: push.e 0
01956: bf 01973
01957: pushi.e 2
01958: pop.v.i self.onebuffer
01960: pushi.e 2
01961: pop.v.i self.twobuffer
01963: pushi.e -5
01964: pushi.e 35
01965: push.v [array]submenucoord
01967: pushi.e 8
01968: cmp.i.v EQ
01969: bf 01973
01970: pushi.e 1
01971: pop.v.i self.controls_quitmenu
01973: push.v self.control_select_con
01975: pushi.e 2
01976: cmp.i.v EQ
01977: bf 02229
01978: push.v self.new_key
01980: pushi.e -1
01981: cmp.i.v NEQ
01982: bf 02150
01983: pushi.e -1
01984: pop.v.i self.dupe
01986: pushi.e 0
01987: pop.v.i self.i
01989: push.v self.i
01991: pushi.e 7
01992: cmp.i.v LT
01993: bf 02015
01994: pushi.e -5
01995: push.v self.i
01997: conv.v.i
01998: push.v [array]input_k
02000: push.v self.new_key
02002: cmp.v.v EQ
02003: bf 02008
02004: push.v self.i
02006: pop.v.v self.dupe
02008: push.v self.i
02010: pushi.e 1
02011: add.i.v
02012: pop.v.v self.i
02014: b 01989
02015: push.v self.dupe
02017: pushi.e 0
02018: cmp.i.v GTE
02019: bf 02034
02020: pushi.e -5
02021: pushi.e -5
02022: pushi.e 35
02023: push.v [array]submenucoord
02025: conv.v.i
02026: push.v [array]input_k
02028: pushi.e -5
02029: push.v self.dupe
02031: conv.v.i
02032: pop.v.v [array]input_k
02034: push.v self.new_key
02036: pushi.e -5
02037: pushi.e -5
02038: pushi.e 35
02039: push.v [array]submenucoord
02041: conv.v.i
02042: pop.v.v [array]input_k
02044: pushi.e -1
02045: pop.v.i self.entercancel
02047: pushi.e -1
02048: pop.v.i self.shiftcancel
02050: pushi.e -1
02051: pop.v.i self.ctrlcancel
02053: pushi.e 0
02054: pop.v.i self.i
02056: push.v self.i
02058: pushi.e 7
02059: cmp.i.v LT
02060: bf 02119
02061: pushi.e -5
02062: push.v self.i
02064: conv.v.i
02065: push.v [array]input_k
02067: pushi.e 13
02068: cmp.i.v EQ
02069: bf 02078
02070: pushi.e -1
02071: pushi.e -5
02072: pushi.e 7
02073: pop.v.i [array]input_k
02075: pushi.e 1
02076: pop.v.i self.entercancel
02078: pushi.e -5
02079: push.v self.i
02081: conv.v.i
02082: push.v [array]input_k
02084: pushi.e 16
02085: cmp.i.v EQ
02086: bf 02095
02087: pushi.e -1
02088: pushi.e -5
02089: pushi.e 8
02090: pop.v.i [array]input_k
02092: pushi.e 1
02093: pop.v.i self.shiftcancel
02095: pushi.e -5
02096: push.v self.i
02098: conv.v.i
02099: push.v [array]input_k
02101: pushi.e 17
02102: cmp.i.v EQ
02103: bf 02112
02104: pushi.e -1
02105: pushi.e -5
02106: pushi.e 9
02107: pop.v.i [array]input_k
02109: pushi.e 1
02110: pop.v.i self.ctrlcancel
02112: push.v self.i
02114: pushi.e 1
02115: add.i.v
02116: pop.v.v self.i
02118: b 02056
02119: push.v self.entercancel
02121: pushi.e -1
02122: cmp.i.v EQ
02123: bf 02129
02124: pushi.e 13
02125: pushi.e -5
02126: pushi.e 7
02127: pop.v.i [array]input_k
02129: push.v self.shiftcancel
02131: pushi.e -1
02132: cmp.i.v EQ
02133: bf 02139
02134: pushi.e 16
02135: pushi.e -5
02136: pushi.e 8
02137: pop.v.i [array]input_k
02139: push.v self.ctrlcancel
02141: pushi.e -1
02142: cmp.i.v EQ
02143: bf 02149
02144: pushi.e 17
02145: pushi.e -5
02146: pushi.e 9
02147: pop.v.i [array]input_k
02149: b 02211
02150: pushi.e -1
02151: pop.v.i self.dupe
02153: pushi.e 0
02154: pop.v.i self.i
02156: push.v self.i
02158: pushi.e 7
02159: cmp.i.v LT
02160: bf 02182
02161: pushi.e -5
02162: push.v self.i
02164: conv.v.i
02165: push.v [array]input_g
02167: push.v self.new_gamepad_key
02169: cmp.v.v EQ
02170: bf 02175
02171: push.v self.i
02173: pop.v.v self.dupe
02175: push.v self.i
02177: pushi.e 1
02178: add.i.v
02179: pop.v.v self.i
02181: b 02156
02182: push.v self.dupe
02184: pushi.e 0
02185: cmp.i.v GTE
02186: bf 02201
02187: pushi.e -5
02188: pushi.e -5
02189: pushi.e 35
02190: push.v [array]submenucoord
02192: conv.v.i
02193: push.v [array]input_g
02195: pushi.e -5
02196: push.v self.dupe
02198: conv.v.i
02199: pop.v.v [array]input_g
02201: push.v self.new_gamepad_key
02203: pushi.e -5
02204: pushi.e -5
02205: pushi.e 35
02206: push.v [array]submenucoord
02208: conv.v.i
02209: pop.v.v [array]input_g
02211: pushi.e 2
02212: pop.v.i self.upbuffer
02214: pushi.e 2
02215: pop.v.i self.downbuffer
02217: pushi.e 2
02218: pop.v.i self.onebuffer
02220: pushi.e 2
02221: pop.v.i self.twobuffer
02223: pushi.e 1
02224: pop.v.i self.selectnoise
02226: pushi.e 0
02227: pop.v.i self.control_select_con
02229: push.v self.controls_quitmenu
02231: pushi.e 1
02232: cmp.i.v EQ
02233: bf 02332
02234: pushi.e 2
02235: pop.v.i self.onebuffer
02237: pushi.e 2
02238: pop.v.i self.twobuffer
02240: push.s ""config_""@7035
02242: pushglb.v global.filechoice
02244: call.i string(argc=1)
02246: add.v.s
02247: push.s "".ini""@7036
02249: add.s.v
02250: call.i ossafe_ini_open(argc=1)
02252: popz.v
02253: pushi.e 0
02254: pop.v.i self.i
02256: push.v self.i
02258: pushi.e 10
02259: cmp.i.v LT
02260: bf 02284
02261: pushi.e -5
02262: push.v self.i
02264: conv.v.i
02265: push.v [array]input_k
02267: push.v self.i
02269: call.i string(argc=1)
02271: push.s ""KEYBOARD_CONTROLS""@7037
02273: conv.s.v
02274: call.i ini_write_real(argc=3)
02276: popz.v
02277: push.v self.i
02279: pushi.e 1
02280: add.i.v
02281: pop.v.v self.i
02283: b 02256
02284: pushi.e 0
02285: pop.v.i self.i
02287: push.v self.i
02289: pushi.e 10
02290: cmp.i.v LT
02291: bf 02315
02292: pushi.e -5
02293: push.v self.i
02295: conv.v.i
02296: push.v [array]input_g
02298: push.v self.i
02300: call.i string(argc=1)
02302: push.s ""GAMEPAD_CONTROLS""@7038
02304: conv.s.v
02305: call.i ini_write_real(argc=3)
02307: popz.v
02308: push.v self.i
02310: pushi.e 1
02311: add.i.v
02312: pop.v.v self.i
02314: b 02287
02315: call.i ossafe_ini_close(argc=0)
02317: popz.v
02318: pushi.e 0
02319: pop.v.i self.controls_quitmenu
02321: pushi.e 0
02322: pop.v.i self.control_select_con
02324: pushi.e 0
02325: pushi.e -5
02326: pushi.e 35
02327: pop.v.i [array]submenucoord
02329: pushi.e 30
02330: pop.v.i global.submenu
02332: pushglb.v global.menuno
02334: pushi.e 4
02335: cmp.i.v EQ
02336: bf 02656
02337: pushglb.v global.submenu
02339: pushi.e 21
02340: cmp.i.v EQ
02341: bf 02526
02342: pushi.e -5
02343: pushi.e -5
02344: pushi.e 20
02345: push.v [array]submenucoord
02347: conv.v.i
02348: push.v [array]char
02350: pop.v.v self.charcoord
02352: call.i up_p(argc=0)
02354: conv.v.b
02355: bf 02372
02356: pushi.e -5
02357: pushi.e 21
02358: push.v [array]submenucoord
02360: pushi.e 0
02361: cmp.i.v GT
02362: bf 02372
02363: pushi.e -5
02364: pushi.e 21
02365: dup.i 1
02366: push.v [array]submenucoord
02368: pushi.e 1
02369: sub.i.v
02370: pop.i.v [array]submenucoord
02372: call.i down_p(argc=0)
02374: conv.v.b
02375: bf 02414
02376: pushi.e -5
02377: pushi.e 21
02378: push.v [array]submenucoord
02380: pushi.e 5
02381: cmp.i.v LT
02382: bf 02414
02383: pushi.e -5
02384: push.v self.charcoord
02386: conv.v.i
02387: break.e -1
02388: push.i 32000
02390: mul.i.i
02391: pushi.e -5
02392: pushi.e 21
02393: push.v [array]submenucoord
02395: pushi.e 1
02396: add.i.v
02397: conv.v.i
02398: break.e -1
02399: add.i.i
02400: push.v [array]spell
02402: pushi.e 0
02403: cmp.i.v NEQ
02404: bf 02414
02405: pushi.e -5
02406: pushi.e 21
02407: dup.i 1
02408: push.v [array]submenucoord
02410: pushi.e 1
02411: add.i.v
02412: pop.i.v [array]submenucoord
02414: call.i button1_p(argc=0)
02416: conv.v.b
02417: bf 02423
02418: push.v self.onebuffer
02420: pushi.e 0
02421: cmp.i.v LT
02422: b 02424
02423: push.e 0
02424: bf 02498
02425: pushi.e -5
02426: push.v self.charcoord
02428: conv.v.i
02429: break.e -1
02430: push.i 32000
02432: mul.i.i
02433: pushi.e -5
02434: pushi.e 21
02435: push.v [array]submenucoord
02437: conv.v.i
02438: break.e -1
02439: add.i.i
02440: push.v [array]spellusable
02442: pushi.e 1
02443: cmp.i.v EQ
02444: bf 02466
02445: pushglb.v global.tension
02447: pushi.e -5
02448: push.v self.charcoord
02450: conv.v.i
02451: break.e -1
02452: push.i 32000
02454: mul.i.i
02455: pushi.e -5
02456: pushi.e 21
02457: push.v [array]submenucoord
02459: conv.v.i
02460: break.e -1
02461: add.i.i
02462: push.v [array]spellcost
02464: cmp.v.v GTE
02465: b 02467
02466: push.e 0
02467: bf 02498
02468: pushi.e -5
02469: push.v self.charcoord
02471: conv.v.i
02472: break.e -1
02473: push.i 32000
02475: mul.i.i
02476: pushi.e -5
02477: pushi.e 21
02478: push.v [array]submenucoord
02480: conv.v.i
02481: break.e -1
02482: add.i.i
02483: push.v [array]spelltarget
02485: pushi.e 1
02486: cmp.i.v EQ
02487: bf 02497
02488: pushi.e 22
02489: pop.v.i global.submenu
02491: pushi.e 2
02492: pop.v.i self.onebuffer
02494: pushi.e 2
02495: pop.v.i self.twobuffer
02497: b 02498
02498: call.i button2_p(argc=0)
02500: conv.v.b
02501: bf 02507
02502: push.v self.twobuffer
02504: pushi.e 0
02505: cmp.i.v LT
02506: b 02508
02507: push.e 0
02508: bf 02526
02509: pushi.e 0
02510: pop.v.i self.deschaver
02512: pushi.e 2
02513: pop.v.i self.onebuffer
02515: pushi.e 2
02516: pop.v.i self.twobuffer
02518: pushi.e 0
02519: pushi.e -5
02520: pushi.e 21
02521: pop.v.i [array]submenucoord
02523: pushi.e 20
02524: pop.v.i global.submenu
02526: pushglb.v global.submenu
02528: pushi.e 20
02529: cmp.i.v EQ
02530: bf 02656
02531: call.i left_p(argc=0)
02533: conv.v.b
02534: bf 02572
02535: pushi.e -5
02536: pushi.e 20
02537: dup.i 1
02538: push.v [array]submenucoord
02540: pushi.e 1
02541: sub.i.v
02542: pop.i.v [array]submenucoord
02544: pushi.e -5
02545: pushi.e 20
02546: push.v [array]submenucoord
02548: pushi.e 0
02549: cmp.i.v LT
02550: bf 02559
02551: push.v self.chartotal
02553: pushi.e 1
02554: sub.i.v
02555: pushi.e -5
02556: pushi.e 20
02557: pop.v.v [array]submenucoord
02559: push.v self.chartotal
02561: pushi.e 2
02562: cmp.i.v GTE
02563: bf 02572
02564: pushi.e 100
02565: conv.i.v
02566: call.i random(argc=1)
02568: call.i ceil(argc=1)
02570: pop.v.v self.dograndom
02572: call.i right_p(argc=0)
02574: conv.v.b
02575: bf 02613
02576: pushi.e -5
02577: pushi.e 20
02578: dup.i 1
02579: push.v [array]submenucoord
02581: pushi.e 1
02582: add.i.v
02583: pop.i.v [array]submenucoord
02585: pushi.e -5
02586: pushi.e 20
02587: push.v [array]submenucoord
02589: push.v self.chartotal
02591: pushi.e 1
02592: sub.i.v
02593: cmp.v.v GT
02594: bf 02600
02595: pushi.e 0
02596: pushi.e -5
02597: pushi.e 20
02598: pop.v.i [array]submenucoord
02600: push.v self.chartotal
02602: pushi.e 2
02603: cmp.i.v GTE
02604: bf 02613
02605: pushi.e 100
02606: conv.i.v
02607: call.i random(argc=1)
02609: call.i ceil(argc=1)
02611: pop.v.v self.dograndom
02613: call.i button1_p(argc=0)
02615: conv.v.b
02616: bf 02622
02617: push.v self.onebuffer
02619: pushi.e 0
02620: cmp.i.v LT
02621: b 02623
02622: push.e 0
02623: bf 02633
02624: pushi.e 1
02625: pop.v.i self.deschaver
02627: pushi.e 21
02628: pop.v.i global.submenu
02630: pushi.e 2
02631: pop.v.i self.onebuffer
02633: call.i button2_p(argc=0)
02635: conv.v.b
02636: bf 02642
02637: push.v self.twobuffer
02639: pushi.e 0
02640: cmp.i.v LT
02641: b 02643
02642: push.e 0
02643: bf 02656
02644: pushi.e 2
02645: pop.v.i self.twobuffer
02647: pushi.e 0
02648: pop.v.i global.menuno
02650: pushi.e 0
02651: pop.v.i global.submenu
02653: pushi.e -1
02654: pop.v.i global.charselect
02656: pushglb.v global.menuno
02658: pushi.e 1
02659: cmp.i.v EQ
02660: bf 03712
02661: pushglb.v global.submenu
02663: pushi.e 2
02664: cmp.i.v EQ
02665: bt 02671
02666: pushglb.v global.submenu
02668: pushi.e 3
02669: cmp.i.v EQ
02670: b 02672
02671: push.e 1
02672: bf 03017
02673: call.i left_p(argc=0)
02675: conv.v.b
02676: bt 02681
02677: call.i right_p(argc=0)
02679: conv.v.b
02680: b 02682
02681: push.e 1
02682: bf 02784
02683: pushi.e -5
02684: pushi.e 2
02685: push.v [array]submenucoord
02687: pop.v.v self.sm
02689: push.v self.sm
02691: pushi.e 0
02692: cmp.i.v EQ
02693: bt 02719
02694: push.v self.sm
02696: pushi.e 2
02697: cmp.i.v EQ
02698: bt 02719
02699: push.v self.sm
02701: pushi.e 4
02702: cmp.i.v EQ
02703: bt 02719
02704: push.v self.sm
02706: pushi.e 6
02707: cmp.i.v EQ
02708: bt 02719
02709: push.v self.sm
02711: pushi.e 8
02712: cmp.i.v EQ
02713: bt 02719
02714: push.v self.sm
02716: pushi.e 10
02717: cmp.i.v EQ
02718: b 02720
02719: push.e 1
02720: bf 02743
02721: pushi.e -5
02722: pushi.e -5
02723: pushi.e 2
02724: push.v [array]submenucoord
02726: pushi.e 1
02727: add.i.v
02728: conv.v.i
02729: push.v [array]item
02731: pushi.e 0
02732: cmp.i.v NEQ
02733: bf 02743
02734: pushi.e -5
02735: pushi.e 2
02736: dup.i 1
02737: push.v [array]submenucoord
02739: pushi.e 1
02740: add.i.v
02741: pop.i.v [array]submenucoord
02743: push.v self.sm
02745: pushi.e 1
02746: cmp.i.v EQ
02747: bt 02773
02748: push.v self.sm
02750: pushi.e 3
02751: cmp.i.v EQ
02752: bt 02773
02753: push.v self.sm
02755: pushi.e 5
02756: cmp.i.v EQ
02757: bt 02773
02758: push.v self.sm
02760: pushi.e 7
02761: cmp.i.v EQ
02762: bt 02773
02763: push.v self.sm
02765: pushi.e 9
02766: cmp.i.v EQ
02767: bt 02773
02768: push.v self.sm
02770: pushi.e 11
02771: cmp.i.v EQ
02772: b 02774
02773: push.e 1
02774: bf 02784
02775: pushi.e -5
02776: pushi.e 2
02777: dup.i 1
02778: push.v [array]submenucoord
02780: pushi.e 1
02781: sub.i.v
02782: pop.i.v [array]submenucoord
02784: call.i down_p(argc=0)
02786: conv.v.b
02787: bf 02915
02788: pushi.e -5
02789: pushi.e 2
02790: push.v [array]submenucoord
02792: pop.v.v self.sm
02794: push.v self.sm
02796: pushi.e 0
02797: cmp.i.v EQ
02798: bt 02819
02799: push.v self.sm
02801: pushi.e 2
02802: cmp.i.v EQ
02803: bt 02819
02804: push.v self.sm
02806: pushi.e 4
02807: cmp.i.v EQ
02808: bt 02819
02809: push.v self.sm
02811: pushi.e 6
02812: cmp.i.v EQ
02813: bt 02819
02814: push.v self.sm
02816: pushi.e 8
02817: cmp.i.v EQ
02818: b 02820
02819: push.e 1
02820: bf 02843
02821: pushi.e -5
02822: pushi.e -5
02823: pushi.e 2
02824: push.v [array]submenucoord
02826: pushi.e 2
02827: add.i.v
02828: conv.v.i
02829: push.v [array]item
02831: pushi.e 0
02832: cmp.i.v NEQ
02833: bf 02843
02834: pushi.e -5
02835: pushi.e 2
02836: dup.i 1
02837: push.v [array]submenucoord
02839: pushi.e 2
02840: add.i.v
02841: pop.i.v [array]submenucoord
02843: push.v self.sm
02845: pushi.e 1
02846: cmp.i.v EQ
02847: bt 02868
02848: push.v self.sm
02850: pushi.e 3
02851: cmp.i.v EQ
02852: bt 02868
02853: push.v self.sm
02855: pushi.e 5
02856: cmp.i.v EQ
02857: bt 02868
02858: push.v self.sm
02860: pushi.e 7
02861: cmp.i.v EQ
02862: bt 02868
02863: push.v self.sm
02865: pushi.e 9
02866: cmp.i.v EQ
02867: b 02869
02868: push.e 1
02869: bf 02915
02870: pushi.e -5
02871: pushi.e -5
02872: pushi.e 2
02873: push.v [array]submenucoord
02875: pushi.e 2
02876: add.i.v
02877: conv.v.i
02878: push.v [array]item
02880: pushi.e 0
02881: cmp.i.v NEQ
02882: bf 02893
02883: pushi.e -5
02884: pushi.e 2
02885: dup.i 1
02886: push.v [array]submenucoord
02888: pushi.e 2
02889: add.i.v
02890: pop.i.v [array]submenucoord
02892: b 02915
02893: pushi.e -5
02894: pushi.e -5
02895: pushi.e 2
02896: push.v [array]submenucoord
02898: pushi.e 1
02899: add.i.v
02900: conv.v.i
02901: push.v [array]item
02903: pushi.e 0
02904: cmp.i.v NEQ
02905: bf 02915
02906: pushi.e -5
02907: pushi.e 2
02908: dup.i 1
02909: push.v [array]submenucoord
02911: pushi.e 1
02912: add.i.v
02913: pop.i.v [array]submenucoord
02915: call.i up_p(argc=0)
02917: conv.v.b
02918: bf 02997
02919: pushi.e -5
02920: pushi.e 2
02921: push.v [array]submenucoord
02923: pop.v.v self.sm
02925: push.v self.sm
02927: pushi.e 2
02928: cmp.i.v EQ
02929: bt 02950
02930: push.v self.sm
02932: pushi.e 4
02933: cmp.i.v EQ
02934: bt 02950
02935: push.v self.sm
02937: pushi.e 6
02938: cmp.i.v EQ
02939: bt 02950
02940: push.v self.sm
02942: pushi.e 8
02943: cmp.i.v EQ
02944: bt 02950
02945: push.v self.sm
02947: pushi.e 10
02948: cmp.i.v EQ
02949: b 02951
02950: push.e 1
02951: bf 02961
02952: pushi.e -5
02953: pushi.e 2
02954: dup.i 1
02955: push.v [array]submenucoord
02957: pushi.e 2
02958: sub.i.v
02959: pop.i.v [array]submenucoord
02961: push.v self.sm
02963: pushi.e 3
02964: cmp.i.v EQ
02965: bt 02986
02966: push.v self.sm
02968: pushi.e 5
02969: cmp.i.v EQ
02970: bt 02986
02971: push.v self.sm
02973: pushi.e 7
02974: cmp.i.v EQ
02975: bt 02986
02976: push.v self.sm
02978: pushi.e 9
02979: cmp.i.v EQ
02980: bt 02986
02981: push.v self.sm
02983: pushi.e 11
02984: cmp.i.v EQ
02985: b 02987
02986: push.e 1
02987: bf 02997
02988: pushi.e -5
02989: pushi.e 2
02990: dup.i 1
02991: push.v [array]submenucoord
02993: pushi.e 2
02994: sub.i.v
02995: pop.i.v [array]submenucoord
02997: call.i button2_p(argc=0)
02999: conv.v.b
03000: bf 03006
03001: push.v self.twobuffer
03003: pushi.e 0
03004: cmp.i.v LT
03005: b 03007
03006: push.e 0
03007: bf 03017
03008: pushi.e 2
03009: pop.v.i self.twobuffer
03011: pushi.e 0
03012: pop.v.i self.deschaver
03014: pushi.e 1
03015: pop.v.i global.submenu
03017: pushglb.v global.submenu
03019: pushi.e 4
03020: cmp.i.v EQ
03021: bf 03412
03022: call.i left_p(argc=0)
03024: conv.v.b
03025: bt 03030
03026: call.i right_p(argc=0)
03028: conv.v.b
03029: b 03031
03030: push.e 1
03031: bf 03133
03032: pushi.e -5
03033: pushi.e 4
03034: push.v [array]submenucoord
03036: pop.v.v self.sm
03038: push.v self.sm
03040: pushi.e 0
03041: cmp.i.v EQ
03042: bt 03068
03043: push.v self.sm
03045: pushi.e 2
03046: cmp.i.v EQ
03047: bt 03068
03048: push.v self.sm
03050: pushi.e 4
03051: cmp.i.v EQ
03052: bt 03068
03053: push.v self.sm
03055: pushi.e 6
03056: cmp.i.v EQ
03057: bt 03068
03058: push.v self.sm
03060: pushi.e 8
03061: cmp.i.v EQ
03062: bt 03068
03063: push.v self.sm
03065: pushi.e 10
03066: cmp.i.v EQ
03067: b 03069
03068: push.e 1
03069: bf 03092
03070: pushi.e -5
03071: pushi.e -5
03072: pushi.e 4
03073: push.v [array]submenucoord
03075: pushi.e 1
03076: add.i.v
03077: conv.v.i
03078: push.v [array]keyitem
03080: pushi.e 0
03081: cmp.i.v NEQ
03082: bf 03092
03083: pushi.e -5
03084: pushi.e 4
03085: dup.i 1
03086: push.v [array]submenucoord
03088: pushi.e 1
03089: add.i.v
03090: pop.i.v [array]submenucoord
03092: push.v self.sm
03094: pushi.e 1
03095: cmp.i.v EQ
03096: bt 03122
03097: push.v self.sm
03099: pushi.e 3
03100: cmp.i.v EQ
03101: bt 03122
03102: push.v self.sm
03104: pushi.e 5
03105: cmp.i.v EQ
03106: bt 03122
03107: push.v self.sm
03109: pushi.e 7
03110: cmp.i.v EQ
03111: bt 03122
03112: push.v self.sm
03114: pushi.e 9
03115: cmp.i.v EQ
03116: bt 03122
03117: push.v self.sm
03119: pushi.e 11
03120: cmp.i.v EQ
03121: b 03123
03122: push.e 1
03123: bf 03133
03124: pushi.e -5
03125: pushi.e 4
03126: dup.i 1
03127: push.v [array]submenucoord
03129: pushi.e 1
03130: sub.i.v
03131: pop.i.v [array]submenucoord
03133: call.i down_p(argc=0)
03135: conv.v.b
03136: bf 03264
03137: pushi.e -5
03138: pushi.e 4
03139: push.v [array]submenucoord
03141: pop.v.v self.sm
03143: push.v self.sm
03145: pushi.e 0
03146: cmp.i.v EQ
03147: bt 03168
03148: push.v self.sm
03150: pushi.e 2
03151: cmp.i.v EQ
03152: bt 03168
03153: push.v self.sm
03155: pushi.e 4
03156: cmp.i.v EQ
03157: bt 03168
03158: push.v self.sm
03160: pushi.e 6
03161: cmp.i.v EQ
03162: bt 03168
03163: push.v self.sm
03165: pushi.e 8
03166: cmp.i.v EQ
03167: b 03169
03168: push.e 1
03169: bf 03192
03170: pushi.e -5
03171: pushi.e -5
03172: pushi.e 4
03173: push.v [array]submenucoord
03175: pushi.e 2
03176: add.i.v
03177: conv.v.i
03178: push.v [array]keyitem
03180: pushi.e 0
03181: cmp.i.v NEQ
03182: bf 03192
03183: pushi.e -5
03184: pushi.e 4
03185: dup.i 1
03186: push.v [array]submenucoord
03188: pushi.e 2
03189: add.i.v
03190: pop.i.v [array]submenucoord
03192: push.v self.sm
03194: pushi.e 1
03195: cmp.i.v EQ
03196: bt 03217
03197: push.v self.sm
03199: pushi.e 3
03200: cmp.i.v EQ
03201: bt 03217
03202: push.v self.sm
03204: pushi.e 5
03205: cmp.i.v EQ
03206: bt 03217
03207: push.v self.sm
03209: pushi.e 7
03210: cmp.i.v EQ
03211: bt 03217
03212: push.v self.sm
03214: pushi.e 9
03215: cmp.i.v EQ
03216: b 03218
03217: push.e 1
03218: bf 03264
03219: pushi.e -5
03220: pushi.e -5
03221: pushi.e 4
03222: push.v [array]submenucoord
03224: pushi.e 2
03225: add.i.v
03226: conv.v.i
03227: push.v [array]keyitem
03229: pushi.e 0
03230: cmp.i.v NEQ
03231: bf 03242
03232: pushi.e -5
03233: pushi.e 4
03234: dup.i 1
03235: push.v [array]submenucoord
03237: pushi.e 2
03238: add.i.v
03239: pop.i.v [array]submenucoord
03241: b 03264
03242: pushi.e -5
03243: pushi.e -5
03244: pushi.e 4
03245: push.v [array]submenucoord
03247: pushi.e 1
03248: add.i.v
03249: conv.v.i
03250: push.v [array]keyitem
03252: pushi.e 0
03253: cmp.i.v NEQ
03254: bf 03264
03255: pushi.e -5
03256: pushi.e 4
03257: dup.i 1
03258: push.v [array]submenucoord
03260: pushi.e 1
03261: add.i.v
03262: pop.i.v [array]submenucoord
03264: call.i up_p(argc=0)
03266: conv.v.b
03267: bf 03346
03268: pushi.e -5
03269: pushi.e 4
03270: push.v [array]submenucoord
03272: pop.v.v self.sm
03274: push.v self.sm
03276: pushi.e 2
03277: cmp.i.v EQ
03278: bt 03299
03279: push.v self.sm
03281: pushi.e 4
03282: cmp.i.v EQ
03283: bt 03299
03284: push.v self.sm
03286: pushi.e 6
03287: cmp.i.v EQ
03288: bt 03299
03289: push.v self.sm
03291: pushi.e 8
03292: cmp.i.v EQ
03293: bt 03299
03294: push.v self.sm
03296: pushi.e 10
03297: cmp.i.v EQ
03298: b 03300
03299: push.e 1
03300: bf 03310
03301: pushi.e -5
03302: pushi.e 4
03303: dup.i 1
03304: push.v [array]submenucoord
03306: pushi.e 2
03307: sub.i.v
03308: pop.i.v [array]submenucoord
03310: push.v self.sm
03312: pushi.e 3
03313: cmp.i.v EQ
03314: bt 03335
03315: push.v self.sm
03317: pushi.e 5
03318: cmp.i.v EQ
03319: bt 03335
03320: push.v self.sm
03322: pushi.e 7
03323: cmp.i.v EQ
03324: bt 03335
03325: push.v self.sm
03327: pushi.e 9
03328: cmp.i.v EQ
03329: bt 03335
03330: push.v self.sm
03332: pushi.e 11
03333: cmp.i.v EQ
03334: b 03336
03335: push.e 1
03336: bf 03346
03337: pushi.e -5
03338: pushi.e 4
03339: dup.i 1
03340: push.v [array]submenucoord
03342: pushi.e 2
03343: sub.i.v
03344: pop.i.v [array]submenucoord
03346: call.i button1_p(argc=0)
03348: conv.v.b
03349: bf 03355
03350: push.v self.onebuffer
03352: pushi.e 0
03353: cmp.i.v LT
03354: b 03356
03355: push.e 0
03356: bf 03392
03357: pushi.e 2
03358: pop.v.i self.onebuffer
03360: pushi.e 1
03361: pop.v.i self.twobuffer
03363: pushi.e -1
03364: push.v self.sm
03366: conv.v.i
03367: push.v [array]keyitemusable
03369: pushi.e 1
03370: cmp.i.v EQ
03371: bf 03387
03372: pushi.e 3
03373: pop.v.i global.charselect
03375: pushi.e -5
03376: push.v self.sm
03378: conv.v.i
03379: push.v [array]keyitem
03381: pushi.e 300
03382: add.i.v
03383: call.i scr_itemuse(argc=1)
03385: popz.v
03386: b 03392
03387: pushi.e 76
03388: conv.i.v
03389: call.i snd_play(argc=1)
03391: popz.v
03392: call.i button2_p(argc=0)
03394: conv.v.b
03395: bf 03401
03396: push.v self.twobuffer
03398: pushi.e 0
03399: cmp.i.v LT
03400: b 03402
03401: push.e 0
03402: bf 03412
03403: pushi.e 2
03404: pop.v.i self.twobuffer
03406: pushi.e 0
03407: pop.v.i self.deschaver
03409: pushi.e 1
03410: pop.v.i global.submenu
03412: pushglb.v global.submenu
03414: pushi.e 3
03415: cmp.i.v EQ
03416: bf 03480
03417: call.i button1_p(argc=0)
03419: conv.v.b
03420: bf 03426
03421: push.v self.onebuffer
03423: pushi.e 0
03424: cmp.i.v LT
03425: b 03427
03426: push.e 0
03427: bf 03434
03428: pushi.e 3
03429: pop.v.i self.onebuffer
03431: pushi.e 7
03432: pop.v.i global.submenu
03434: pushi.e -5
03435: pushi.e -5
03436: pushi.e 2
03437: push.v [array]submenucoord
03439: conv.v.i
03440: push.v [array]item
03442: pushi.e 0
03443: cmp.i.v EQ
03444: bf 03480
03445: pushi.e -5
03446: pushi.e 2
03447: push.v [array]submenucoord
03449: pushi.e 0
03450: cmp.i.v EQ
03451: bf 03464
03452: pushi.e 1
03453: pop.v.i global.submenu
03455: pushi.e 0
03456: pop.v.i self.deschaver
03458: pushi.e 2
03459: pop.v.i self.twobuffer
03461: pushi.e 2
03462: pop.v.i self.onebuffer
03464: pushi.e -5
03465: pushi.e 2
03466: push.v [array]submenucoord
03468: pushi.e 0
03469: cmp.i.v GT
03470: bf 03480
03471: pushi.e -5
03472: pushi.e 2
03473: dup.i 1
03474: push.v [array]submenucoord
03476: pushi.e 1
03477: sub.i.v
03478: pop.i.v [array]submenucoord
03480: pushglb.v global.submenu
03482: pushi.e 2
03483: cmp.i.v EQ
03484: bf 03572
03485: call.i button1_p(argc=0)
03487: conv.v.b
03488: bf 03494
03489: push.v self.onebuffer
03491: pushi.e 0
03492: cmp.i.v LT
03493: b 03495
03494: push.e 0
03495: bf 03526
03496: pushi.e 3
03497: pop.v.i self.onebuffer
03499: pushi.e -5
03500: pushi.e -5
03501: pushi.e 2
03502: push.v [array]submenucoord
03504: conv.v.i
03505: push.v [array]item
03507: call.i scr_iteminfo(argc=1)
03509: popz.v
03510: push.v self.itemtarget
03512: pushi.e 1
03513: cmp.i.v EQ
03514: bf 03518
03515: pushi.e 5
03516: pop.v.i global.submenu
03518: push.v self.itemtarget
03520: pushi.e 2
03521: cmp.i.v EQ
03522: bf 03526
03523: pushi.e 6
03524: pop.v.i global.submenu
03526: pushi.e -5
03527: pushi.e -5
03528: pushi.e 2
03529: push.v [array]submenucoord
03531: conv.v.i
03532: push.v [array]item
03534: pushi.e 0
03535: cmp.i.v EQ
03536: bf 03572
03537: pushi.e -5
03538: pushi.e 2
03539: push.v [array]submenucoord
03541: pushi.e 0
03542: cmp.i.v EQ
03543: bf 03556
03544: pushi.e 1
03545: pop.v.i global.submenu
03547: pushi.e 0
03548: pop.v.i self.deschaver
03550: pushi.e 2
03551: pop.v.i self.twobuffer
03553: pushi.e 2
03554: pop.v.i self.onebuffer
03556: pushi.e -5
03557: pushi.e 2
03558: push.v [array]submenucoord
03560: pushi.e 0
03561: cmp.i.v GT
03562: bf 03572
03563: pushi.e -5
03564: pushi.e 2
03565: dup.i 1
03566: push.v [array]submenucoord
03568: pushi.e 1
03569: sub.i.v
03570: pop.i.v [array]submenucoord
03572: pushglb.v global.submenu
03574: pushi.e 1
03575: cmp.i.v EQ
03576: bf 03712
03577: call.i left_p(argc=0)
03579: conv.v.b
03580: bf 03609
03581: pushi.e -5
03582: pushi.e 1
03583: push.v [array]submenucoord
03585: pushi.e 0
03586: cmp.i.v EQ
03587: bf 03597
03588: pushi.e 2
03589: pushi.e -5
03590: pushi.e 1
03591: pop.v.i [array]submenucoord
03593: pushi.e 1
03594: pop.v.i self.movenoise
03596: b 03609
03597: pushi.e -5
03598: pushi.e 1
03599: dup.i 1
03600: push.v [array]submenucoord
03602: pushi.e 1
03603: sub.i.v
03604: pop.i.v [array]submenucoord
03606: pushi.e 1
03607: pop.v.i self.movenoise
03609: call.i right_p(argc=0)
03611: conv.v.b
03612: bf 03641
03613: pushi.e -5
03614: pushi.e 1
03615: push.v [array]submenucoord
03617: pushi.e 2
03618: cmp.i.v EQ
03619: bf 03629
03620: pushi.e 0
03621: pushi.e -5
03622: pushi.e 1
03623: pop.v.i [array]submenucoord
03625: pushi.e 1
03626: pop.v.i self.movenoise
03628: b 03641
03629: pushi.e -5
03630: pushi.e 1
03631: dup.i 1
03632: push.v [array]submenucoord
03634: pushi.e 1
03635: add.i.v
03636: pop.i.v [array]submenucoord
03638: pushi.e 1
03639: pop.v.i self.movenoise
03641: call.i button1_p(argc=0)
03643: conv.v.b
03644: bf 03692
03645: pushi.e -5
03646: pushi.e 1
03647: push.v [array]submenucoord
03649: pushi.e 2
03650: add.i.v
03651: pop.v.v global.submenu
03653: pushglb.v global.submenu
03655: pushi.e 4
03656: cmp.i.v EQ
03657: bf 03661
03658: pushi.e 1
03659: pop.v.i self.deschaver
03661: pushglb.v global.submenu
03663: pushi.e 2
03664: cmp.i.v EQ
03665: bt 03671
03666: pushglb.v global.submenu
03668: pushi.e 3
03669: cmp.i.v EQ
03670: b 03672
03671: push.e 1
03672: bf 03692
03673: pushi.e 1
03674: pop.v.i self.deschaver
03676: call.i scr_itemdesc(argc=0)
03678: popz.v
03679: pushi.e -5
03680: pushi.e 0
03681: push.v [array]item
03683: pushi.e 0
03684: cmp.i.v EQ
03685: bf 03692
03686: pushi.e 1
03687: pop.v.i global.submenu
03689: pushi.e 0
03690: pop.v.i self.deschaver
03692: call.i button2_p(argc=0)
03694: conv.v.b
03695: bf 03701
03696: push.v self.twobuffer
03698: pushi.e 0
03699: cmp.i.v LT
03700: b 03702
03701: push.e 0
03702: bf 03712
03703: pushi.e 2
03704: pop.v.i self.twobuffer
03706: pushi.e 0
03707: pop.v.i global.menuno
03709: pushi.e 0
03710: pop.v.i global.submenu
03712: pushglb.v global.menuno
03714: pushi.e 2
03715: cmp.i.v EQ
03716: bf 04674
03717: pushglb.v global.submenu
03719: pushi.e 12
03720: cmp.i.v EQ
03721: bt 03732
03722: pushglb.v global.submenu
03724: pushi.e 13
03725: cmp.i.v EQ
03726: bt 03732
03727: pushglb.v global.submenu
03729: pushi.e 14
03730: cmp.i.v EQ
03731: b 03733
03732: push.e 1
03733: bf 04459
03734: pushi.e 0
03735: pop.v.i self._up_pressed
03737: call.i up_h(argc=0)
03739: conv.v.b
03740: bf 03850
03741: call.i up_p(argc=0)
03743: conv.v.b
03744: bf 03748
03745: pushi.e 1
03746: pop.v.i self._up_pressed
03748: push.v self.hold_up
03750: pushi.e 1
03751: add.i.v
03752: pop.v.v self.hold_up
03754: push.v self.hold_up
03756: pushi.e 8
03757: cmp.i.v GTE
03758: bf 03765
03759: pushi.e 1
03760: pop.v.i self._up_pressed
03762: pushi.e 6
03763: pop.v.i self.hold_up
03765: push.v self._up_pressed
03767: pushi.e 1
03768: cmp.i.v EQ
03769: bf 03849
03770: pushi.e -5
03771: pushglb.v global.submenu
03773: conv.v.i
03774: push.v [array]submenucoord
03776: pushi.e 0
03777: cmp.i.v GT
03778: bf 03790
03779: pushi.e -5
03780: pushglb.v global.submenu
03782: conv.v.i
03783: dup.i 1
03784: push.v [array]submenucoord
03786: pushi.e 1
03787: sub.i.v
03788: pop.i.v [array]submenucoord
03790: pushglb.v global.submenu
03792: pushi.e 12
03793: cmp.i.v EQ
03794: bf 03816
03795: pushi.e -5
03796: pushglb.v global.submenu
03798: conv.v.i
03799: push.v [array]submenucoord
03801: pushi.e -1
03802: pushi.e 0
03803: push.v [array]pagemax
03805: cmp.v.v LT
03806: bf 03816
03807: pushi.e -1
03808: pushi.e 0
03809: dup.i 1
03810: push.v [array]pagemax
03812: pushi.e 1
03813: sub.i.v
03814: pop.i.v [array]pagemax
03816: pushglb.v global.submenu
03818: pushi.e 13
03819: cmp.i.v EQ
03820: bt 03826
03821: pushglb.v global.submenu
03823: pushi.e 14
03824: cmp.i.v EQ
03825: b 03827
03826: push.e 1
03827: bf 03849
03828: pushi.e -5
03829: pushglb.v global.submenu
03831: conv.v.i
03832: push.v [array]submenucoord
03834: pushi.e -1
03835: pushi.e 1
03836: push.v [array]pagemax
03838: cmp.v.v LT
03839: bf 03849
03840: pushi.e -1
03841: pushi.e 1
03842: dup.i 1
03843: push.v [array]pagemax
03845: pushi.e 1
03846: sub.i.v
03847: pop.i.v [array]pagemax
03849: b 03853
03850: pushi.e 0
03851: pop.v.i self.hold_up
03853: pushi.e 0
03854: pop.v.i self._down_pressed
03856: call.i down_h(argc=0)
03858: conv.v.b
03859: bf 04039
03860: call.i down_p(argc=0)
03862: pushi.e 1
03863: cmp.i.v EQ
03864: bf 03868
03865: pushi.e 1
03866: pop.v.i self._down_pressed
03868: push.v self.hold_down
03870: pushi.e 1
03871: add.i.v
03872: pop.v.v self.hold_down
03874: push.v self.hold_down
03876: pushi.e 8
03877: cmp.i.v GTE
03878: bf 03885
03879: pushi.e 1
03880: pop.v.i self._down_pressed
03882: pushi.e 6
03883: pop.v.i self.hold_down
03885: pushi.e -5
03886: pushglb.v global.submenu
03888: conv.v.i
03889: push.v [array]submenucoord
03891: pushi.e 11
03892: cmp.i.v LT
03893: bf 03899
03894: push.v self._down_pressed
03896: pushi.e 1
03897: cmp.i.v EQ
03898: b 03900
03899: push.e 0
03900: bf 04038
03901: pushglb.v global.submenu
03903: pushi.e 12
03904: cmp.i.v EQ
03905: bf 03920
03906: pushi.e -5
03907: pushi.e -5
03908: pushglb.v global.submenu
03910: pushi.e 1
03911: add.i.v
03912: conv.v.i
03913: push.v [array]submenucoord
03915: conv.v.i
03916: push.v [array]weapon
03918: pop.v.v self.nextone
03920: pushglb.v global.submenu
03922: pushi.e 13
03923: cmp.i.v EQ
03924: bt 03930
03925: pushglb.v global.submenu
03927: pushi.e 14
03928: cmp.i.v EQ
03929: b 03931
03930: push.e 1
03931: bf 03946
03932: pushi.e -5
03933: pushi.e -5
03934: pushglb.v global.submenu
03936: pushi.e 1
03937: add.i.v
03938: conv.v.i
03939: push.v [array]submenucoord
03941: conv.v.i
03942: push.v [array]armor
03944: pop.v.v self.nextone
03946: pushi.e -5
03947: pushglb.v global.submenu
03949: conv.v.i
03950: dup.i 1
03951: push.v [array]submenucoord
03953: pushi.e 1
03954: add.i.v
03955: pop.i.v [array]submenucoord
03957: pushglb.v global.submenu
03959: pushi.e 12
03960: cmp.i.v EQ
03961: bf 03994
03962: pushi.e -5
03963: pushglb.v global.submenu
03965: conv.v.i
03966: push.v [array]submenucoord
03968: pushi.e -1
03969: pushi.e 0
03970: push.v [array]pagemax
03972: pushi.e 5
03973: add.i.v
03974: cmp.v.v GT
03975: bf 03983
03976: pushi.e -1
03977: pushi.e 0
03978: push.v [array]pagemax
03980: pushi.e 6
03981: cmp.i.v LT
03982: b 03984
03983: push.e 0
03984: bf 03994
03985: pushi.e -1
03986: pushi.e 0
03987: dup.i 1
03988: push.v [array]pagemax
03990: pushi.e 1
03991: add.i.v
03992: pop.i.v [array]pagemax
03994: pushglb.v global.submenu
03996: pushi.e 13
03997: cmp.i.v EQ
03998: bt 04004
03999: pushglb.v global.submenu
04001: pushi.e 14
04002: cmp.i.v EQ
04003: b 04005
04004: push.e 1
04005: bf 04038
04006: pushi.e -5
04007: pushglb.v global.submenu
04009: conv.v.i
04010: push.v [array]submenucoord
04012: pushi.e -1
04013: pushi.e 1
04014: push.v [array]pagemax
04016: pushi.e 5
04017: add.i.v
04018: cmp.v.v GT
04019: bf 04027
04020: pushi.e -1
04021: pushi.e 1
04022: push.v [array]pagemax
04024: pushi.e 6
04025: cmp.i.v LT
04026: b 04028
04027: push.e 0
04028: bf 04038
04029: pushi.e -1
04030: pushi.e 1
04031: dup.i 1
04032: push.v [array]pagemax
04034: pushi.e 1
04035: add.i.v
04036: pop.i.v [array]pagemax
04038: b 04042
04039: pushi.e 0
04040: pop.v.i self.hold_down
04042: call.i button1_p(argc=0)
04044: conv.v.b
04045: bf 04051
04046: push.v self.onebuffer
04048: pushi.e 0
04049: cmp.i.v LT
04050: b 04052
04051: push.e 0
04052: bf 04433
04053: pushi.e 5
04054: pop.v.i self.onebuffer
04056: pushi.e 0
04057: pop.v.i self.canequip
04059: pushi.e -5
04060: pushi.e -5
04061: pushi.e 10
04062: push.v [array]submenucoord
04064: conv.v.i
04065: push.v [array]char
04067: pop.v.v self.wwho
04069: push.s "" ""@24
04071: pop.v.s self.wmsg
04073: pushglb.v global.submenu
04075: pushi.e 12
04076: cmp.i.v EQ
04077: bf 04154
04078: pushi.e -5
04079: pushi.e -5
04080: pushglb.v global.submenu
04082: conv.v.i
04083: push.v [array]submenucoord
04085: conv.v.i
04086: push.v [array]weapon
04088: call.i scr_weaponinfo(argc=1)
04090: popz.v
04091: push.v self.wwho
04093: pushi.e 2
04094: cmp.i.v EQ
04095: bf 04100
04096: push.v self.wmessage2temp
04098: pop.v.v self.wmsg
04100: push.v self.wwho
04102: pushi.e 3
04103: cmp.i.v EQ
04104: bf 04109
04105: push.v self.wmessage3temp
04107: pop.v.v self.wmsg
04109: push.v self.wwho
04111: pushi.e 1
04112: cmp.i.v EQ
04113: bf 04119
04114: push.v self.weaponchar1temp
04116: pushi.e 1
04117: cmp.i.v EQ
04118: b 04120
04119: push.e 0
04120: bf 04124
04121: pushi.e 1
04122: pop.v.i self.canequip
04124: push.v self.wwho
04126: pushi.e 2
04127: cmp.i.v EQ
04128: bf 04134
04129: push.v self.weaponchar2temp
04131: pushi.e 1
04132: cmp.i.v EQ
04133: b 04135
04134: push.e 0
04135: bf 04139
04136: pushi.e 1
04137: pop.v.i self.canequip
04139: push.v self.wwho
04141: pushi.e 3
04142: cmp.i.v EQ
04143: bf 04149
04144: push.v self.weaponchar3temp
04146: pushi.e 1
04147: cmp.i.v EQ
04148: b 04150
04149: push.e 0
04150: bf 04154
04151: pushi.e 1
04152: pop.v.i self.canequip
04154: pushglb.v global.submenu
04156: pushi.e 13
04157: cmp.i.v EQ
04158: bt 04164
04159: pushglb.v global.submenu
04161: pushi.e 14
04162: cmp.i.v EQ
04163: b 04165
04164: push.e 1
04165: bf 04242
04166: pushi.e -5
04167: pushi.e -5
04168: pushglb.v global.submenu
04170: conv.v.i
04171: push.v [array]submenucoord
04173: conv.v.i
04174: push.v [array]armor
04176: call.i scr_armorinfo(argc=1)
04178: popz.v
04179: push.v self.wwho
04181: pushi.e 2
04182: cmp.i.v EQ
04183: bf 04188
04184: push.v self.amessage2temp
04186: pop.v.v self.wmsg
04188: push.v self.wwho
04190: pushi.e 3
04191: cmp.i.v EQ
04192: bf 04197
04193: push.v self.amessage3temp
04195: pop.v.v self.wmsg
04197: push.v self.wwho
04199: pushi.e 1
04200: cmp.i.v EQ
04201: bf 04207
04202: push.v self.armorchar1temp
04204: pushi.e 1
04205: cmp.i.v EQ
04206: b 04208
04207: push.e 0
04208: bf 04212
04209: pushi.e 1
04210: pop.v.i self.canequip
04212: push.v self.wwho
04214: pushi.e 2
04215: cmp.i.v EQ
04216: bf 04222
04217: push.v self.armorchar2temp
04219: pushi.e 1
04220: cmp.i.v EQ
04221: b 04223
04222: push.e 0
04223: bf 04227
04224: pushi.e 1
04225: pop.v.i self.canequip
04227: push.v self.wwho
04229: pushi.e 3
04230: cmp.i.v EQ
04231: bf 04237
04232: push.v self.armorchar3temp
04234: pushi.e 1
04235: cmp.i.v EQ
04236: b 04238
04237: push.e 0
04238: bf 04242
04239: pushi.e 1
04240: pop.v.i self.canequip
04242: push.v self.canequip
04244: pushi.e 1
04245: cmp.i.v EQ
04246: bf 04419
04247: pushi.e 0
04248: pop.v.i self.hold_up
04250: pushi.e 0
04251: pop.v.i self.hold_down
04253: pushi.e 75
04254: conv.i.v
04255: call.i snd_play(argc=1)
04257: popz.v
04258: pushglb.v global.submenu
04260: pushi.e 12
04261: cmp.i.v EQ
04262: bf 04315
04263: pushi.e -5
04264: push.v self.wwho
04266: conv.v.i
04267: push.v [array]charweapon
04269: pop.v.v self.oldequip
04271: pushi.e -5
04272: pushi.e -5
04273: pushglb.v global.submenu
04275: conv.v.i
04276: push.v [array]submenucoord
04278: conv.v.i
04279: push.v [array]weapon
04281: pop.v.v self.newequip
04283: push.v self.newequip
04285: pushi.e -5
04286: push.v self.wwho
04288: conv.v.i
04289: pop.v.v [array]charweapon
04291: push.v self.oldequip
04293: pushi.e -5
04294: pushi.e -5
04295: pushglb.v global.submenu
04297: conv.v.i
04298: push.v [array]submenucoord
04300: conv.v.i
04301: pop.v.v [array]weapon
04303: call.i scr_weaponinfo_mine(argc=0)
04305: popz.v
04306: call.i scr_weaponinfo_all(argc=0)
04308: popz.v
04309: pushi.e 2
04310: pop.v.i self.twobuffer
04312: pushi.e 11
04313: pop.v.i global.submenu
04315: pushglb.v global.submenu
04317: pushi.e 13
04318: cmp.i.v EQ
04319: bt 04325
04320: pushglb.v global.submenu
04322: pushi.e 14
04323: cmp.i.v EQ
04324: b 04326
04325: push.e 1
04326: bf 04418
04327: pushglb.v global.submenu
04329: pushi.e 13
04330: cmp.i.v EQ
04331: bf 04340
04332: pushi.e -5
04333: push.v self.wwho
04335: conv.v.i
04336: push.v [array]chararmor1
04338: pop.v.v self.oldequip
04340: pushglb.v global.submenu
04342: pushi.e 14
04343: cmp.i.v EQ
04344: bf 04353
04345: pushi.e -5
04346: push.v self.wwho
04348: conv.v.i
04349: push.v [array]chararmor2
04351: pop.v.v self.oldequip
04353: pushi.e -5
04354: pushi.e -5
04355: pushglb.v global.submenu
04357: conv.v.i
04358: push.v [array]submenucoord
04360: conv.v.i
04361: push.v [array]armor
04363: pop.v.v self.newequip
04365: pushglb.v global.submenu
04367: pushi.e 13
04368: cmp.i.v EQ
04369: bf 04378
04370: push.v self.newequip
04372: pushi.e -5
04373: push.v self.wwho
04375: conv.v.i
04376: pop.v.v [array]chararmor1
04378: pushglb.v global.submenu
04380: pushi.e 14
04381: cmp.i.v EQ
04382: bf 04391
04383: push.v self.newequip
04385: pushi.e -5
04386: push.v self.wwho
04388: conv.v.i
04389: pop.v.v [array]chararmor2
04391: push.v self.oldequip
04393: pushi.e -5
04394: pushi.e -5
04395: pushglb.v global.submenu
04397: conv.v.i
04398: push.v [array]submenucoord
04400: conv.v.i
04401: pop.v.v [array]armor
04403: call.i scr_armorinfo_mine(argc=0)
04405: popz.v
04406: call.i scr_armorinfo_all(argc=0)
04408: popz.v
04409: pushi.e 2
04410: pop.v.i self.twobuffer
04412: call.i scr_dmenu_armor_selection_match(argc=0)
04414: popz.v
04415: pushi.e 11
04416: pop.v.i global.submenu
04418: b 04424
04419: pushi.e 76
04420: conv.i.v
04421: call.i snd_play(argc=1)
04423: popz.v
04424: push.v self.wmsg
04426: pushi.e -5
04427: pushi.e 10
04428: push.v [array]submenucoord
04430: call.i scr_itemcomment(argc=2)
04432: popz.v
04433: call.i button2_p(argc=0)
04435: conv.v.b
04436: bf 04442
04437: push.v self.twobuffer
04439: pushi.e 0
04440: cmp.i.v LT
04441: b 04443
04442: push.e 0
04443: bf 04459
04444: pushi.e 0
04445: pop.v.i self.hold_up
04447: pushi.e 0
04448: pop.v.i self.hold_down
04450: pushi.e 2
04451: pop.v.i self.twobuffer
04453: call.i scr_dmenu_armor_selection_match(argc=0)
04455: popz.v
04456: pushi.e 11
04457: pop.v.i global.submenu
04459: pushglb.v global.submenu
04461: pushi.e 11
04462: cmp.i.v EQ
04463: bf 04559
04464: call.i up_p(argc=0)
04466: conv.v.b
04467: bf 04489
04468: pushi.e -5
04469: pushi.e 11
04470: dup.i 1
04471: push.v [array]submenucoord
04473: pushi.e 1
04474: sub.i.v
04475: pop.i.v [array]submenucoord
04477: pushi.e -5
04478: pushi.e 11
04479: push.v [array]submenucoord
04481: pushi.e -1
04482: cmp.i.v EQ
04483: bf 04489
04484: pushi.e 2
04485: pushi.e -5
04486: pushi.e 11
04487: pop.v.i [array]submenucoord
04489: call.i down_p(argc=0)
04491: conv.v.b
04492: bf 04514
04493: pushi.e -5
04494: pushi.e 11
04495: dup.i 1
04496: push.v [array]submenucoord
04498: pushi.e 1
04499: add.i.v
04500: pop.i.v [array]submenucoord
04502: pushi.e -5
04503: pushi.e 11
04504: push.v [array]submenucoord
04506: pushi.e 3
04507: cmp.i.v EQ
04508: bf 04514
04509: pushi.e 0
04510: pushi.e -5
04511: pushi.e 11
04512: pop.v.i [array]submenucoord
04514: call.i button1_p(argc=0)
04516: conv.v.b
04517: bf 04523
04518: push.v self.onebuffer
04520: pushi.e 0
04521: cmp.i.v LT
04522: b 04524
04523: push.e 0
04524: bf 04539
04525: pushi.e 2
04526: pop.v.i self.onebuffer
04528: pushi.e 12
04529: pushi.e -5
04530: pushi.e 11
04531: push.v [array]submenucoord
04533: add.v.i
04534: pop.v.v global.submenu
04536: call.i scr_dmenu_armor_selection_match(argc=0)
04538: popz.v
04539: call.i button2_p(argc=0)
04541: conv.v.b
04542: bf 04548
04543: push.v self.twobuffer
04545: pushi.e 0
04546: cmp.i.v LT
04547: b 04549
04548: push.e 0
04549: bf 04559
04550: pushi.e 0
04551: pop.v.i self.deschaver
04553: pushi.e 2
04554: pop.v.i self.twobuffer
04556: pushi.e 10
04557: pop.v.i global.submenu
04559: pushglb.v global.submenu
04561: pushi.e 10
04562: cmp.i.v EQ
04563: bf 04674
04564: call.i left_p(argc=0)
04566: conv.v.b
04567: bf 04592
04568: pushi.e -5
04569: pushi.e 10
04570: dup.i 1
04571: push.v [array]submenucoord
04573: pushi.e 1
04574: sub.i.v
04575: pop.i.v [array]submenucoord
04577: pushi.e -5
04578: pushi.e 10
04579: push.v [array]submenucoord
04581: pushi.e 0
04582: cmp.i.v LT
04583: bf 04592
04584: push.v self.chartotal
04586: pushi.e 1
04587: sub.i.v
04588: pushi.e -5
04589: pushi.e 10
04590: pop.v.v [array]submenucoord
04592: call.i right_p(argc=0)
04594: conv.v.b
04595: bf 04620
04596: pushi.e -5
04597: pushi.e 10
04598: dup.i 1
04599: push.v [array]submenucoord
04601: pushi.e 1
04602: add.i.v
04603: pop.i.v [array]submenucoord
04605: pushi.e -5
04606: pushi.e 10
04607: push.v [array]submenucoord
04609: push.v self.chartotal
04611: pushi.e 1
04612: sub.i.v
04613: cmp.v.v GT
04614: bf 04620
04615: pushi.e 0
04616: pushi.e -5
04617: pushi.e 10
04618: pop.v.i [array]submenucoord
04620: pushi.e -5
04621: pushi.e 10
04622: push.v [array]submenucoord
04624: pop.v.v global.charselect
04626: call.i button1_p(argc=0)
04628: conv.v.b
04629: bf 04635
04630: push.v self.onebuffer
04632: pushi.e 0
04633: cmp.i.v LT
04634: b 04636
04635: push.e 0
04636: bf 04651
04637: pushi.e 1
04638: pop.v.i self.deschaver
04640: pushi.e 0
04641: pushi.e -5
04642: pushi.e 11
04643: pop.v.i [array]submenucoord
04645: pushi.e 11
04646: pop.v.i global.submenu
04648: pushi.e 2
04649: pop.v.i self.onebuffer
04651: call.i button2_p(argc=0)
04653: conv.v.b
04654: bf 04660
04655: push.v self.twobuffer
04657: pushi.e 0
04658: cmp.i.v LT
04659: b 04661
04660: push.e 0
04661: bf 04674
04662: pushi.e 2
04663: pop.v.i self.twobuffer
04665: pushi.e 0
04666: pop.v.i global.menuno
04668: pushi.e 0
04669: pop.v.i global.submenu
04671: pushi.e -1
04672: pop.v.i global.charselect
04674: pushglb.v global.menuno
04676: pushi.e 0
04677: cmp.i.v EQ
04678: bf 04980
04679: pushi.e 0
04680: pop.v.i global.submenu
04682: call.i left_p(argc=0)
04684: conv.v.b
04685: bf 04730
04686: pushi.e -5
04687: pushi.e 0
04688: push.v [array]menucoord
04690: pushi.e 0
04691: cmp.i.v EQ
04692: bf 04702
04693: pushi.e 4
04694: pushi.e -5
04695: pushi.e 0
04696: pop.v.i [array]menucoord
04698: pushi.e 1
04699: pop.v.i self.movenoise
04701: b 04730
04702: pushi.e -5
04703: pushi.e 0
04704: dup.i 1
04705: push.v [array]menucoord
04707: pushi.e 1
04708: sub.i.v
04709: pop.i.v [array]menucoord
04711: pushi.e -5
04712: pushi.e 0
04713: push.v [array]menucoord
04715: pushi.e 2
04716: cmp.i.v EQ
04717: bf 04727
04718: pushi.e -5
04719: pushi.e 0
04720: dup.i 1
04721: push.v [array]menucoord
04723: pushi.e 1
04724: sub.i.v
04725: pop.i.v [array]menucoord
04727: pushi.e 1
04728: pop.v.i self.movenoise
04730: call.i right_p(argc=0)
04732: conv.v.b
04733: bf 04778
04734: pushi.e -5
04735: pushi.e 0
04736: push.v [array]menucoord
04738: pushi.e 4
04739: cmp.i.v EQ
04740: bf 04750
04741: pushi.e 0
04742: pushi.e -5
04743: pushi.e 0
04744: pop.v.i [array]menucoord
04746: pushi.e 1
04747: pop.v.i self.movenoise
04749: b 04778
04750: pushi.e -5
04751: pushi.e 0
04752: dup.i 1
04753: push.v [array]menucoord
04755: pushi.e 1
04756: add.i.v
04757: pop.i.v [array]menucoord
04759: pushi.e -5
04760: pushi.e 0
04761: push.v [array]menucoord
04763: pushi.e 2
04764: cmp.i.v EQ
04765: bf 04775
04766: pushi.e -5
04767: pushi.e 0
04768: dup.i 1
04769: push.v [array]menucoord
04771: pushi.e 1
04772: add.i.v
04773: pop.i.v [array]menucoord
04775: pushi.e 1
04776: pop.v.i self.movenoise
04778: call.i button1_p(argc=0)
04780: conv.v.b
04781: bf 04787
04782: push.v self.onebuffer
04784: pushi.e 0
04785: cmp.i.v LT
04786: b 04788
04787: push.e 0
04788: bf 04921
04789: pushi.e 2
04790: pop.v.i self.onebuffer
04792: pushi.e -5
04793: pushi.e 0
04794: push.v [array]menucoord
04796: pushi.e 1
04797: add.i.v
04798: pop.v.v global.menuno
04800: pushglb.v global.menuno
04802: pushi.e 1
04803: cmp.i.v EQ
04804: bf 04828
04805: pushi.e 1
04806: pop.v.i global.submenu
04808: pushi.e 0
04809: pushi.e -5
04810: pushi.e 1
04811: pop.v.i [array]submenucoord
04813: pushi.e 0
04814: pushi.e -5
04815: pushi.e 2
04816: pop.v.i [array]submenucoord
04818: pushi.e 0
04819: pushi.e -5
04820: pushi.e 3
04821: pop.v.i [array]submenucoord
04823: pushi.e 0
04824: pushi.e -5
04825: pushi.e 4
04826: pop.v.i [array]submenucoord
04828: pushglb.v global.menuno
04830: pushi.e 2
04831: cmp.i.v EQ
04832: bf 04889
04833: call.i scr_weaponinfo_all(argc=0)
04835: popz.v
04836: call.i scr_armorinfo_all(argc=0)
04838: popz.v
04839: call.i scr_weaponinfo_mine(argc=0)
04841: popz.v
04842: call.i scr_armorinfo_mine(argc=0)
04844: popz.v
04845: pushi.e 0
04846: pushi.e -5
04847: pushi.e 10
04848: pop.v.i [array]submenucoord
04850: pushi.e 0
04851: pushi.e -5
04852: pushi.e 11
04853: pop.v.i [array]submenucoord
04855: pushi.e 0
04856: pushi.e -5
04857: pushi.e 12
04858: pop.v.i [array]submenucoord
04860: pushi.e 0
04861: pushi.e -5
04862: pushi.e 13
04863: pop.v.i [array]submenucoord
04865: pushi.e 0
04866: pushi.e -5
04867: pushi.e 14
04868: pop.v.i [array]submenucoord
04870: pushi.e 0
04871: pushi.e -1
04872: pushi.e 0
04873: pop.v.i [array]pagemax
04875: pushi.e 0
04876: pushi.e -1
04877: pushi.e 1
04878: pop.v.i [array]pagemax
04880: pushi.e 10
04881: pop.v.i global.submenu
04883: pushi.e -5
04884: pushi.e 10
04885: push.v [array]submenucoord
04887: pop.v.v global.charselect
04889: pushglb.v global.menuno
04891: pushi.e 3
04892: cmp.i.v EQ
04893: bf 04897
04894: pushi.e 0
04895: pop.v.i global.menuno
04897: pushglb.v global.menuno
04899: pushi.e 4
04900: cmp.i.v EQ
04901: bf 04908
04902: pushi.e 20
04903: pop.v.i global.submenu
04905: call.i scr_spellinfo_all(argc=0)
04907: popz.v
04908: pushglb.v global.menuno
04910: pushi.e 5
04911: cmp.i.v EQ
04912: bf 04921
04913: pushi.e 30
04914: pop.v.i global.submenu
04916: pushi.e 0
04917: pushi.e -5
04918: pushi.e 30
04919: pop.v.i [array]submenucoord
04921: pushi.e 0
04922: pop.v.i self.close
04924: call.i button2_p(argc=0)
04926: conv.v.b
04927: bf 04933
04928: push.v self.twobuffer
04930: pushi.e 0
04931: cmp.i.v LT
04932: b 04934
04933: push.e 0
04934: bf 04938
04935: pushi.e 1
04936: pop.v.i self.close
04938: call.i button3_p(argc=0)
04940: conv.v.b
04941: bf 04947
04942: push.v self.threebuffer
04944: pushi.e 0
04945: cmp.i.v LT
04946: b 04948
04947: push.e 0
04948: bf 04952
04949: pushi.e 1
04950: pop.v.i self.close
04952: push.v self.close
04954: pushi.e 1
04955: cmp.i.v EQ
04956: bf 04980
04957: pushglb.v global.menuno
04959: pushi.e 0
04960: cmp.i.v EQ
04961: bf 04980
04962: pushi.e -1
04963: pop.v.i global.menuno
04965: pushi.e 0
04966: pop.v.i global.interact
04968: pushi.e 0
04969: pop.v.i self.charcon
04971: pushi.e 326
04972: pushenv 04979
04973: pushi.e 2
04974: pop.v.i self.threebuffer
04976: pushi.e 2
04977: pop.v.i self.twobuffer
04979: popenv 04973
04980: pushglb.v global.interact
04982: pushi.e 6
04983: cmp.i.v EQ
04984: bf 04995
04985: pushi.e 5
04986: conv.i.v
04987: call.i instance_exists(argc=1)
04989: conv.v.b
04990: not.b
04991: bf 04995
04992: pushi.e 0
04993: pop.v.i global.interact
04995: push.v self.charcon
04997: pushi.e 1
04998: cmp.i.v EQ
04999: bf 05109
05000: pushi.e 1
05001: pop.v.i self.drawchar
05003: pushi.e 60
05004: pop.v.i self.bpy
05006: pushi.e 80
05007: pop.v.i self.tpy
05009: pushglb.v global.interact
05011: pushi.e 5
05012: cmp.i.v EQ
05013: bf 05058
05014: push.v self.tp
05016: push.v self.tpy
05018: pushi.e 1
05019: sub.i.v
05020: cmp.v.v LT
05021: bf 05054
05022: push.v self.tpy
05024: push.v self.tp
05026: sub.v.v
05027: pushi.e 40
05028: cmp.i.v LTE
05029: bf 05047
05030: push.v self.tp
05032: push.v self.tpy
05034: push.v self.tp
05036: sub.v.v
05037: push.d 2.5
05040: div.d.v
05041: call.i round(argc=1)
05043: add.v.v
05044: pop.v.v self.tp
05046: b 05053
05047: push.v self.tp
05049: pushi.e 30
05050: add.i.v
05051: pop.v.v self.tp
05053: b 05058
05054: push.v self.tpy
05056: pop.v.v self.tp
05058: push.v self.bp
05060: push.v self.bpy
05062: pushi.e 1
05063: sub.i.v
05064: cmp.v.v LT
05065: bf 05071
05066: push.v self.charcon
05068: pushi.e 1
05069: cmp.i.v EQ
05070: b 05072
05071: push.e 0
05072: bf 05105
05073: push.v self.bpy
05075: push.v self.bp
05077: sub.v.v
05078: pushi.e 40
05079: cmp.i.v LTE
05080: bf 05098
05081: push.v self.bp
05083: push.v self.bpy
05085: push.v self.bp
05087: sub.v.v
05088: push.d 2.5
05091: div.d.v
05092: call.i round(argc=1)
05094: add.v.v
05095: pop.v.v self.bp
05097: b 05104
05098: push.v self.bp
05100: pushi.e 30
05101: add.i.v
05102: pop.v.v self.bp
05104: b 05109
05105: push.v self.bpy
05107: pop.v.v self.bp
05109: push.v self.charcon
05111: pushi.e 0
05112: cmp.i.v EQ
05113: bf 05190
05114: push.v self.tp
05116: pushi.e 0
05117: cmp.i.v GT
05118: bf 05145
05119: push.v self.tp
05121: pushi.e 80
05122: cmp.i.v GTE
05123: bf 05138
05124: push.v self.tp
05126: push.v self.tp
05128: push.d 2.5
05131: div.d.v
05132: call.i round(argc=1)
05134: sub.v.v
05135: pop.v.v self.tp
05137: b 05144
05138: push.v self.tp
05140: pushi.e 30
05141: sub.i.v
05142: pop.v.v self.tp
05144: b 05148
05145: pushi.e 0
05146: pop.v.i self.tp
05148: push.v self.bp
05150: pushi.e 0
05151: cmp.i.v GT
05152: bf 05179
05153: push.v self.bp
05155: pushi.e 40
05156: cmp.i.v GTE
05157: bf 05172
05158: push.v self.bp
05160: push.v self.bp
05162: push.d 2.5
05165: div.d.v
05166: call.i round(argc=1)
05168: sub.v.v
05169: pop.v.v self.bp
05171: b 05178
05172: push.v self.bp
05174: pushi.e 30
05175: sub.i.v
05176: pop.v.v self.bp
05178: b 05182
05179: pushi.e 0
05180: pop.v.i self.bp
05182: push.v self.bp
05184: pushi.e 0
05185: cmp.i.v EQ
05186: bf 05190
05187: pushi.e 0
05188: pop.v.i self.drawchar
05190: push.v self.movenoise
05192: pushi.e 1
05193: cmp.i.v EQ
05194: bf 05203
05195: pushi.e 149
05196: conv.i.v
05197: call.i snd_play(argc=1)
05199: popz.v
05200: pushi.e 0
05201: pop.v.i self.movenoise
05203: push.v self.selectnoise
05205: pushi.e 1
05206: cmp.i.v EQ
05207: bf 05216
05208: pushi.e 150
05209: conv.i.v
05210: call.i snd_play(argc=1)
05212: popz.v
05213: pushi.e 0
05214: pop.v.i self.selectnoise
05216: push.v self.onebuffer
05218: pushi.e 1
05219: sub.i.v
05220: pop.v.v self.onebuffer
05222: push.v self.twobuffer
05224: pushi.e 1
05225: sub.i.v
05226: pop.v.v self.twobuffer
05228: push.v self.threebuffer
05230: pushi.e 1
05231: sub.i.v
05232: pop.v.v self.threebuffer
05234: push.v self.upbuffer
05236: pushi.e 1
05237: sub.i.v
05238: pop.v.v self.upbuffer
05240: push.v self.downbuffer
05242: pushi.e 1
05243: sub.i.v
05244: pop.v.v self.downbuffer
05246: call.i scr_debug(argc=0)
05248: conv.v.b
05249: bf func_end
05250: pushi.e 83
05251: conv.i.v
05252: call.i keyboard_check_pressed(argc=1)
05254: conv.v.b
05255: bf 05265
05256: pushi.e 323
05257: conv.i.v
05258: pushi.e 0
05259: conv.i.v
05260: pushi.e 0
05261: conv.i.v
05262: call.i instance_create(argc=3)
05264: popz.v
05265: pushi.e 76
05266: conv.i.v
05267: call.i keyboard_check_pressed(argc=1)
05269: conv.v.b
05270: bf 05274
05271: call.i scr_load(argc=0)
05273: popz.v
05274: pushi.e 82
05275: conv.i.v
05276: call.i keyboard_check_pressed(argc=1)
05278: conv.v.b
05279: bf func_end
05280: call.i game_restart_true(argc=0)
05282: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.Step, EventSubtypeStep.BeginStep, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.v global.time
00002: pushi.e 1
00003: add.i.v
00004: pop.v.v global.time
00006: call.i scr_debug(argc=0)
00008: conv.v.b
00009: bf 00043
00010: pushi.e 1
00011: conv.i.v
00012: call.i scr_84_debug(argc=1)
00014: conv.v.b
00015: bf 00017
00016: exit.i
00017: pushi.e 121
00018: conv.i.v
00019: call.i keyboard_check_pressed(argc=1)
00021: conv.v.b
00022: bf 00043
00023: push.v self.screenshot_number
00025: call.i string(argc=1)
00027: push.s ""_shot.png""@9980
00029: add.s.v
00030: pop.v.v self.screen_name
00032: push.v self.screen_name
00034: call.i screen_save(argc=1)
00036: popz.v
00037: push.v self.screenshot_number
00039: pushi.e 1
00040: add.i.v
00041: pop.v.v self.screenshot_number
00043: pushi.e 27
00044: conv.i.v
00045: call.i keyboard_check(argc=1)
00047: conv.v.b
00048: bf 00072
00049: push.v self.quit_timer
00051: pushi.e 0
00052: cmp.i.v LT
00053: bf 00057
00054: pushi.e 0
00055: pop.v.i self.quit_timer
00057: push.v self.quit_timer
00059: pushi.e 1
00060: add.i.v
00061: pop.v.v self.quit_timer
00063: push.v self.quit_timer
00065: pushi.e 30
00066: cmp.i.v GTE
00067: bf 00071
00068: call.i ossafe_game_end(argc=0)
00070: popz.v
00071: b 00078
00072: push.v self.quit_timer
00074: pushi.e 2
00075: sub.i.v
00076: pop.v.v self.quit_timer
00078: pushi.e 115
00079: conv.i.v
00080: call.i keyboard_check_pressed(argc=1)
00082: conv.v.b
00083: bf 00087
00084: pushi.e 1
00085: pop.v.i self.fullscreen_toggle
00087: push.v self.fullscreen_toggle
00089: pushi.e 1
00090: cmp.i.v EQ
00091: bf 00150
00092: pushi.e 0
00093: pop.v.i self.fullscreen_toggle
00095: call.i window_get_fullscreen(argc=0)
00097: conv.v.b
00098: bf 00125
00099: pushi.e 0
00100: conv.i.v
00101: call.i window_set_fullscreen(argc=1)
00103: popz.v
00104: push.s ""true_config.ini""@3518
00106: conv.s.v
00107: call.i ossafe_ini_open(argc=1)
00109: popz.v
00110: pushi.e 0
00111: conv.i.v
00112: push.s ""FULLSCREEN""@9984
00114: conv.s.v
00115: push.s ""SCREEN""@9985
00117: conv.s.v
00118: call.i ini_write_real(argc=3)
00120: popz.v
00121: call.i ossafe_ini_close(argc=0)
00123: popz.v
00124: b 00150
00125: pushi.e 1
00126: conv.i.v
00127: call.i window_set_fullscreen(argc=1)
00129: popz.v
00130: push.s ""true_config.ini""@3518
00132: conv.s.v
00133: call.i ossafe_ini_open(argc=1)
00135: popz.v
00136: pushi.e 1
00137: conv.i.v
00138: push.s ""FULLSCREEN""@9984
00140: conv.s.v
00141: push.s ""SCREEN""@9985
00143: conv.s.v
00144: call.i ini_write_real(argc=3)
00146: popz.v
00147: call.i ossafe_ini_close(argc=0)
00149: popz.v
00150: push.v self.window_center_toggle
00152: pushi.e 2
00153: cmp.i.v EQ
00154: bf 00161
00155: call.i window_center(argc=0)
00157: popz.v
00158: pushi.e 0
00159: pop.v.i self.window_center_toggle
00161: push.v self.window_center_toggle
00163: pushi.e 1
00164: cmp.i.v EQ
00165: bf 00169
00166: pushi.e 2
00167: pop.v.i self.window_center_toggle
00169: pushi.e 0
00170: pop.v.i self.i
00172: push.v self.i
00174: pushi.e 10
00175: cmp.i.v LT
00176: bf 00198
00177: pushi.e 0
00178: pushi.e -5
00179: push.v self.i
00181: conv.v.i
00182: pop.v.i [array]input_released
00184: pushi.e 0
00185: pushi.e -5
00186: push.v self.i
00188: conv.v.i
00189: pop.v.i [array]input_pressed
00191: push.v self.i
00193: pushi.e 1
00194: add.i.v
00195: pop.v.v self.i
00197: b 00172
00198: push.v self.gamepad_check_timer
00200: pushi.e 1
00201: add.i.v
00202: pop.v.v self.gamepad_check_timer
00204: push.v self.gamepad_check_timer
00206: pushi.e 90
00207: cmp.i.v GTE
00208: bf 00228
00209: pushi.e 0
00210: conv.i.v
00211: call.i gamepad_is_connected(argc=1)
00213: conv.v.b
00214: bf 00222
00215: pushi.e 1
00216: pop.v.i self.gamepad_active
00218: pushi.e 0
00219: pop.v.i self.gamepad_id
00221: b 00225
00222: pushi.e 0
00223: pop.v.i self.gamepad_active
00225: pushi.e 0
00226: pop.v.i self.gamepad_check_timer
00228: push.v self.gamepad_active
00230: pushi.e 1
00231: cmp.i.v EQ
00232: bf 00414
00233: pushi.e 0
00234: pop.v.i self.i
00236: push.v self.i
00238: pushi.e 4
00239: cmp.i.v LT
00240: bf 00327
00241: pushi.e -5
00242: push.v self.i
00244: conv.v.i
00245: push.v [array]input_k
00247: call.i keyboard_check(argc=1)
00249: conv.v.b
00250: bt 00271
00251: pushi.e -5
00252: push.v self.i
00254: conv.v.i
00255: push.v [array]input_g
00257: pushi.e 0
00258: conv.i.v
00259: call.i gamepad_button_check(argc=2)
00261: conv.v.b
00262: bt 00271
00263: push.v self.i
00265: pushi.e 0
00266: conv.i.v
00267: call.i scr_gamepad_axis_check(argc=2)
00269: conv.v.b
00270: b 00272
00271: push.e 1
00272: bf 00297
00273: pushi.e -5
00274: push.v self.i
00276: conv.v.i
00277: push.v [array]input_held
00279: pushi.e 0
00280: cmp.i.v EQ
00281: bf 00289
00282: pushi.e 1
00283: pushi.e -5
00284: push.v self.i
00286: conv.v.i
00287: pop.v.i [array]input_pressed
00289: pushi.e 1
00290: pushi.e -5
00291: push.v self.i
00293: conv.v.i
00294: pop.v.i [array]input_held
00296: b 00320
00297: pushi.e -5
00298: push.v self.i
00300: conv.v.i
00301: push.v [array]input_held
00303: pushi.e 1
00304: cmp.i.v EQ
00305: bf 00313
00306: pushi.e 1
00307: pushi.e -5
00308: push.v self.i
00310: conv.v.i
00311: pop.v.i [array]input_released
00313: pushi.e 0
00314: pushi.e -5
00315: push.v self.i
00317: conv.v.i
00318: pop.v.i [array]input_held
00320: push.v self.i
00322: pushi.e 1
00323: add.i.v
00324: pop.v.v self.i
00326: b 00236
00327: pushi.e 4
00328: pop.v.i self.i
00330: push.v self.i
00332: pushi.e 10
00333: cmp.i.v LT
00334: bf 00413
00335: pushi.e -5
00336: push.v self.i
00338: conv.v.i
00339: push.v [array]input_k
00341: call.i keyboard_check(argc=1)
00343: conv.v.b
00344: bt 00357
00345: pushi.e -5
00346: push.v self.i
00348: conv.v.i
00349: push.v [array]input_g
00351: pushi.e 0
00352: conv.i.v
00353: call.i gamepad_button_check(argc=2)
00355: conv.v.b
00356: b 00358
00357: push.e 1
00358: bf 00383
00359: pushi.e -5
00360: push.v self.i
00362: conv.v.i
00363: push.v [array]input_held
00365: pushi.e 0
00366: cmp.i.v EQ
00367: bf 00375
00368: pushi.e 1
00369: pushi.e -5
00370: push.v self.i
00372: conv.v.i
00373: pop.v.i [array]input_pressed
00375: pushi.e 1
00376: pushi.e -5
00377: push.v self.i
00379: conv.v.i
00380: pop.v.i [array]input_held
00382: b 00406
00383: pushi.e -5
00384: push.v self.i
00386: conv.v.i
00387: push.v [array]input_held
00389: pushi.e 1
00390: cmp.i.v EQ
00391: bf 00399
00392: pushi.e 1
00393: pushi.e -5
00394: push.v self.i
00396: conv.v.i
00397: pop.v.i [array]input_released
00399: pushi.e 0
00400: pushi.e -5
00401: push.v self.i
00403: conv.v.i
00404: pop.v.i [array]input_held
00406: push.v self.i
00408: pushi.e 1
00409: add.i.v
00410: pop.v.v self.i
00412: b 00330
00413: b func_end
00414: pushi.e 0
00415: pop.v.i self.i
00417: push.v self.i
00419: pushi.e 10
00420: cmp.i.v LT
00421: bf func_end
00422: pushi.e -5
00423: push.v self.i
00425: conv.v.i
00426: push.v [array]input_k
00428: call.i keyboard_check(argc=1)
00430: conv.v.b
00431: bf 00456
00432: pushi.e -5
00433: push.v self.i
00435: conv.v.i
00436: push.v [array]input_held
00438: pushi.e 0
00439: cmp.i.v EQ
00440: bf 00448
00441: pushi.e 1
00442: pushi.e -5
00443: push.v self.i
00445: conv.v.i
00446: pop.v.i [array]input_pressed
00448: pushi.e 1
00449: pushi.e -5
00450: push.v self.i
00452: conv.v.i
00453: pop.v.i [array]input_held
00455: b 00479
00456: pushi.e -5
00457: push.v self.i
00459: conv.v.i
00460: push.v [array]input_held
00462: pushi.e 1
00463: cmp.i.v EQ
00464: bf 00472
00465: pushi.e 1
00466: pushi.e -5
00467: push.v self.i
00469: conv.v.i
00470: pop.v.i [array]input_released
00472: pushi.e 0
00473: pushi.e -5
00474: push.v self.i
00476: conv.v.i
00477: pop.v.i [array]input_held
00479: push.v self.i
00481: pushi.e 1
00482: add.i.v
00483: pop.v.v self.i
00485: b 00417
", Data.Functions, Data.Variables, Data.Strings));

Data.Scripts.ByName("scr_change_language")?.Code.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.lang
00002: push.s ""en""@2775
00004: cmp.s.v EQ
00005: bf 00011
00006: push.s ""ja""@1566
00008: pop.v.s global.lang
00010: b 00015
00011: push.s ""en""@2775
00013: pop.v.s global.lang
00015: push.s ""true_config.ini""@3518
00017: conv.s.v
00018: call.i ossafe_ini_open(argc=1)
00020: popz.v
00021: pushglb.v global.lang
00023: push.s ""LANG""@3519
00025: conv.s.v
00026: push.s ""LANG""@3519
00028: conv.s.v
00029: call.i ini_write_string(argc=3)
00031: popz.v
00032: call.i ossafe_ini_close(argc=0)
00034: popz.v
00035: call.i scr_84_init_localization(argc=0)
00037: popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.GameObjects.ByName("obj_initializer2").EventHandlerFor(EventType.Create, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: push.s ""true_config.ini""@3518
00002: conv.s.v
00003: call.i ossafe_ini_open(argc=1)
00005: popz.v
00006: push.s ""en""@2775
00008: conv.s.v
00009: push.s ""LANG""@3519
00011: conv.s.v
00012: push.s ""LANG""@3519
00014: conv.s.v
00015: call.i ini_read_string(argc=3)
00017: pop.v.v global.lang
00019: call.i ossafe_ini_close(argc=0)
00021: popz.v
00022: pushi.e 0
00023: conv.i.v
00024: pushi.e 20
00025: conv.i.v
00026: push.s ""0123456789""@7398
00028: conv.s.v
00029: pushi.e 677
00030: conv.i.v
00031: call.i font_add_sprite_ext(argc=4)
00033: pop.v.v global.damagefont
00035: pushi.e 2
00036: conv.i.v
00037: pushi.e 0
00038: conv.i.v
00039: push.s ""obj_initializer2_slash_Create_0_gml_2_0""@10050
00041: conv.s.v
00042: call.i scr_84_get_lang_string(argc=1)
00044: pushi.e 907
00045: conv.i.v
00046: call.i font_add_sprite_ext(argc=4)
00048: pop.v.v global.hpfont
00050: call.i scr_gamestart(argc=0)
00052: popz.v
00053: pushi.e 0
00054: pop.v.i self.i
00056: push.v self.i
00058: pushi.e 100
00059: cmp.i.v LT
00060: bf 00075
00061: pushi.e 0
00062: pushi.e -5
00063: push.v self.i
00065: conv.v.i
00066: pop.v.i [array]tempflag
00068: push.v self.i
00070: pushi.e 1
00071: add.i.v
00072: pop.v.v self.i
00074: b 00056
00075: pushi.e 300
00076: pop.v.i global.heartx
00078: pushi.e 220
00079: pop.v.i global.hearty
00081: pushi.e 1
00082: conv.i.v
00083: call.i audio_group_load(argc=1)
00085: popz.v
00086: pushi.e 320
00087: conv.i.v
00088: call.i instance_exists(argc=1)
00090: conv.v.b
00091: not.b
00092: bf func_end
00093: pushi.e 320
00094: conv.i.v
00095: pushi.e 0
00096: conv.i.v
00097: pushi.e 0
00098: conv.i.v
00099: call.i instance_create(argc=3)
00101: popz.v
", Data.Functions, Data.Variables, Data.Strings));

//The End 
ScriptMessage("Patched! ;D");
