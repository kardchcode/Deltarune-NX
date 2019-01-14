EnsureDataLoaded();

ScriptMessage("DELTARUNE patcher for the Nintendo Switch\nv0.8 (alpha)");

//STABLE Script

//The game will no longer request internet access when launching it :D
Data.GeneralInfo.GMS2AllowStatistics = false;

//Fix collision problems in the rest of interactable objects (quick hack)
Data.GameObjects.ByName("obj_interactablesolid").ParentId = Data.GameObjects.ByName("obj_solidlong");

//Fix some collisions being "transparent" (another ugly hack but hey)
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
 
//Fix left-stick up and down being inverted:

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
var savedata_save = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_savedata_save") }; //new
var savedata_load = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_savedata_load") }; //new
var ini_open = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ini_open") };
var ini_close = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ini_close") };
var game_end = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_game_end") };
var fill_rectangle = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_fill_rectangle") };  //new TODO: Implement borders
var file_text_writeln = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_writeln") };
var file_text_write_string = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_write_string") };
var file_text_write_real = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_write_real") };
var file_text_readln = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_readln") };
var file_text_read_string = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_read_string") };
var file_text_read_real = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_read_real") };
var file_text_open_write = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_open_write") };
var file_text_open_read = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_open_read") };
var file_text_eof = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_eof") };
var file_text_close = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_text_close") };
var file_exists = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_exists") };
var file_delete = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_file_delete") };
var substr = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_substr") }; //new
var strlen = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_strlen") }; //new

//Ensure the missing GLOBAL variables
Data.Variables.EnsureDefined("osflavor", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_id", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("switchlogin", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);  //Not necessary yet but ya now...
Data.Variables.EnsureDefined("savedata", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_buffer", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_load", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_debuginfo", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("current_ini", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);

//Ensure the misssing SELF variables
Data.Variables.EnsureDefined("undefined", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemat", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemdf", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemmag", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itembolts", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazeamt", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazesize", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemboltspeed", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemspecial", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("os_type", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("text", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("lines", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("handle", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

//Define some LOCAL variables (divided for each script)
var var_json = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "json", Data.Strings, Data)); //savedata_save

var var_name = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "name", Data.Strings, Data)); //ini_open
var var_file = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "file", Data.Strings, Data)); 
var var_data = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "data", Data.Strings, Data));

var var_x1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "x1", Data.Strings, Data)); //fill_rectangle
var var_y1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "y1", Data.Strings, Data));
var var_x2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "x2", Data.Strings, Data));
var var_y2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(4, "y2", Data.Strings, Data));
var var_temp = Data.Variables.IndexOf(Data.Variables.DefineLocal(5, "temp", Data.Strings, Data));

var var_handle = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_writeln

var var_handle1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_write_string

var var_handle2 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_write_real

var var_handle3 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_readln
var var_line3 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle4 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_read_string
var var_line4 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle5 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_read_real
var var_line5 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "line", Data.Strings, Data)); 

var var_handle6 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_open_write

var var_name1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "name", Data.Strings, Data)); //file_text_open_read
var var_file1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "file", Data.Strings, Data)); 
var var_data1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "data", Data.Strings, Data));
var var_num_lines1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(4, "num_lines", Data.Strings, Data)); 
var var_newline_pos1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(5, "newline_pos", Data.Strings, Data));
var var_nextline_pos1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(6, "nextline_pos", Data.Strings, Data));
var var_line1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(7, "line", Data.Strings, Data));
var var_lines1 = Data.Variables.IndexOf(Data.Variables.DefineLocal(8, "lines", Data.Strings, Data));

var var_handle7 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_eof

var var_handle8 = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "handle", Data.Strings, Data)); //file_text_close

var var_str = Data.Variables.IndexOf(Data.Variables.DefineLocal(1, "str", Data.Strings, Data)); //substr
var var_pos = Data.Variables.IndexOf(Data.Variables.DefineLocal(2, "pos", Data.Strings, Data));
var var_len = Data.Variables.IndexOf(Data.Variables.DefineLocal(3, "len", Data.Strings, Data));

//Ensure the missing functions
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

//Quick hot-fix for this specific undeclared strings
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
Data.Strings.MakeString("empty string"); //13225

//Set osflavor and savedata TODO: implement obj_time_Other_72 code for obj_time to set savedata dynamically
Data.Scripts.ByName("__init_global")?.Code.Append(Assembler.Assemble(@"
pushi.e 5
pop.v.i global.osflavor
call.i ds_map_create(argc=0)
pop.v.i global.savedata
", Data.Functions, Data.Variables, Data.Strings));

// Part 1, adding new functions and scripts
//TODO: Add CodeLocals Local entries

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

savedata_save.Append(Assembler.Assemble(@"
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
Data.Code.Add(savedata_save);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = savedata_save.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("savedata_save"), Code = savedata_save });
Data.Functions.EnsureDefined("savedata_save", Data.Strings);

savedata_load.Append(Assembler.Assemble(@"
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
Data.Code.Add(savedata_load);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = savedata_load.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("savedata_load"), Code = savedata_load });
Data.Functions.EnsureDefined("savedata_load", Data.Strings);

ini_open.Append(Assembler.Assemble(@"
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
Data.Code.Add(ini_open);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ini_open.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ini_open"), Code = ini_open });
Data.Functions.EnsureDefined("ini_open", Data.Strings);

ini_close.Append(Assembler.Assemble(@"
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
Data.Code.Add(ini_close);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = ini_close.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("ini_close"), Code = ini_close });
Data.Functions.EnsureDefined("ini_close", Data.Strings);

game_end.Append(Assembler.Assemble(@"
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
Data.Code.Add(game_end);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = game_end.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("game_end"), Code = game_end });
Data.Functions.EnsureDefined("game_end", Data.Strings);

//Will try to add borders like undertale later on
fill_rectangle.Append(Assembler.Assemble(@"
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
Data.Code.Add(fill_rectangle);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = fill_rectangle.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("fill_rectangle"), Code = fill_rectangle });
Data.Functions.EnsureDefined("fill_rectangle", Data.Strings);

file_text_writeln.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_writeln);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_writeln.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_writeln"), Code = file_text_writeln });
Data.Functions.EnsureDefined("file_text_writeln", Data.Strings);

file_text_write_string.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_write_string);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_write_string.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_write_string"), Code = file_text_write_string });
Data.Functions.EnsureDefined("file_text_write_string", Data.Strings);

file_text_write_real.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_write_real);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_write_real.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_write_real"), Code = file_text_write_real });
Data.Functions.EnsureDefined("file_text_write_real", Data.Strings);

file_text_readln.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_readln);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_readln.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_readln"), Code = file_text_readln });
Data.Functions.EnsureDefined("file_text_readln", Data.Strings);

file_text_read_string.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_read_string);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_read_string.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_read_string"), Code = file_text_read_string });
Data.Functions.EnsureDefined("file_text_read_string", Data.Strings);

file_text_read_real.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_read_real);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_read_real.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_read_real"), Code = file_text_read_real });
Data.Functions.EnsureDefined("file_text_read_real", Data.Strings);

file_text_open_write.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_open_write);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_open_write.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_open_write"), Code = file_text_open_write });
Data.Functions.EnsureDefined("file_text_open_write", Data.Strings);

//Todo: remove the hacky hack for global.osflavor (now handled by __init_global)
file_text_open_read.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_open_read);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_open_read.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_open_read"), Code = file_text_open_read });
Data.Functions.EnsureDefined("file_text_open_read", Data.Strings);

file_text_eof.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_eof);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_eof.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_eof"), Code = file_text_eof });
Data.Functions.EnsureDefined("file_text_eof", Data.Strings); 

file_text_close.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_text_close);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_text_close.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_text_close"), Code = file_text_close });
Data.Functions.EnsureDefined("file_text_close", Data.Strings);

file_exists.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_exists);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_exists.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_exists"), Code = file_exists });
Data.Functions.EnsureDefined("file_exists", Data.Strings);

file_delete.Append(Assembler.Assemble(@"
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
Data.Code.Add(file_delete);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = file_delete.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("file_delete"), Code = file_delete });
Data.Functions.EnsureDefined("file_delete", Data.Strings);

//Part 2, replacing necessary file calls and adding some hacky hacks

//adding savedata calls
Data.Scripts.ByName("scr_save")?.Code.Append(Assembler.Assemble(@"
call.i savedata_save(argc=0)
popz.v
", Data.Functions, Data.Variables, Data.Strings));

Data.Scripts.ByName("scr_load")?.Code.Append(Assembler.Assemble(@"
call.i savedata_load(argc=0)
popz.v
", Data.Functions, Data.Variables, Data.Strings));

ScriptMessage("Patched!");
