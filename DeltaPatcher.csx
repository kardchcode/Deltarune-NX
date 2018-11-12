// Fixes the inverted up and down directions, messes some collisions and... fixes tha saveprocess? v0.1

EnsureDataLoaded();

ScriptMessage("DELTARUNE patcher for the Nintendo Switch\nv0.1");

//Fix the rest of the controlls!
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
00075: pushi.e 1 //TODO, change strings from press x to close to press b
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

//Fix some collisions:
Data.Rooms.ByName("room_krishallway").GameObjects.Add(new UndertaleRoom.GameObject(){   
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = 55, Y = 165, ScaleX = 21});

Data.Rooms.ByName("room_torhouse").GameObjects.Add(new UndertaleRoom.GameObject(){   
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = 76, Y = 200, ScaleX = 28});

Data.Rooms.ByName("room_dark_eyepuzzle").GameObjects.Add(new UndertaleRoom.GameObject(){  
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = -20, Y = 300, ScaleX = 70});

Data.Rooms.ByName("room_dark_eyepuzzle").GameObjects.Add(new UndertaleRoom.GameObject(){  
 InstanceID = Data.GeneralInfo.LastObj++,
 ObjectDefinition = Data.GameObjects.ByName("obj_solidblock"),
 X = -20, Y = 400, ScaleX = 70});

//Fix left-stick up and down inverted:

//Declaring code names
var up_p = Data.Scripts.ByName("up_p")?.Code;
var up_h = Data.Scripts.ByName("up_h")?.Code;
var down_p = Data.Scripts.ByName("down_p")?.Code;
var down_h = Data.Scripts.ByName("down_h")?.Code;

//Up pressed
up_p.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Up held
up_h.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Down pressed
down_p.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Down held
down_h.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

//Fix savedata:
//Declaring and creating necessary variables for code-strings-functions-whatever (extra and ossafe) TODO: LocalsCount argument for each script?
var ossafe_savedata_save = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_savedata_save") };
var ossafe_savedata_load = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_savedata_load") };
var ossafe_ini_open = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_ini_open") };
var ossafe_ini_close = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_ini_close") };
var ossafe_game_end = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_game_end") };
var ossafe_fill_rectangle = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_fill_rectangle") };
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

//Ensure the missing GLOBAL variables
Data.Variables.EnsureDefined("osflavor", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_id", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("switchlogin", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);  //Not necessary yet...
Data.Variables.EnsureDefined("savedata", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_buffer", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_load", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_debuginfo", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("current_ini", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);

//Ensure the misssing SELF variable TODO: these shouldn't pop an error w/o declaring them...
Data.Variables.EnsureDefined("undefined", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemat", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemdf", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemmag", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itembolts", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazeamt", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemgrazesize", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemboltspeed", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("itemspecial", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

//Side note, some scripts, functions and variables get cloned because of this
Data.Variables.EnsureDefined("os_type", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("text", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("lines", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);
Data.Variables.EnsureDefined("handle", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

//Define some LOCAL variables (divided for each script)
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

//Ensure the missing functions TODO: define scripts for this functions?
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

//Quick hot-fix for this specific undeclared string
Data.Strings.MakeString("is_write");
Data.Strings.MakeString("line_read");
Data.Strings.MakeString("Deltarune");
Data.Strings.MakeString("showdialog");
Data.Strings.MakeString("savepadindex");
Data.Strings.MakeString("slottitle");
Data.Strings.MakeString("Deltarune Save Data"); 
Data.Strings.MakeString("subtitle");
Data.Strings.MakeString("deltarune.sav");
Data.Strings.MakeString("load in progress");
Data.Strings.MakeString("save in progress");

//Set osflavor to value 5, basically means it's the switch console (hot-fix to avoid future problems)
Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.Create, Data.Strings, Data.Code, Data.CodeLocals).Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushi.e 5
00001: pop.v.i global.osflavor
00003: pushi.e 0
00004: pop.v.i self.quit_timer
00006: pushi.e 1
00007: pop.v.i self.keyboard_active
00009: pushi.e 1
00010: pop.v.i self.gamepad_active
00012: pushi.e 0
00013: pop.v.i self.gamepad_check_timer
00015: pushi.e 0
00016: pop.v.i self.gamepad_id
00018: push.d 0.4
00021: pop.v.d self.axis_value
00023: pushi.e 0
00024: pop.v.i self.fullscreen_toggle
00026: pushi.e 0
00027: pop.v.i self.window_center_toggle
00029: pushi.e 0
00030: pop.v.i self.screenshot_number
00032: pushi.e 320
00033: conv.i.v
00034: call.i instance_number(argc=1)
00036: pushi.e 1
00037: cmp.i.v GT
00038: bf 00043
00039: call.i instance_destroy(argc=0)
00041: popz.v
00042: b func_end
00043: call.i display_get_height(argc=0)
00045: pop.v.v self.display_height
00047: call.i display_get_width(argc=0)
00049: pop.v.v self.display_width
00051: pushi.e 1
00052: pop.v.i self.window_size_multiplier
00054: pushi.e 2
00055: pop.v.i self._ww
00057: push.v self._ww
00059: pushi.e 6
00060: cmp.i.v LT
00061: bf 00091
00062: push.v self.display_width
00064: pushi.e 640
00065: push.v self._ww
00067: mul.v.i
00068: cmp.v.v GT
00069: bf 00078
00070: push.v self.display_height
00072: pushi.e 480
00073: push.v self._ww
00075: mul.v.i
00076: cmp.v.v GT
00077: b 00079
00078: push.e 0
00079: bf 00084
00080: push.v self._ww
00082: pop.v.v self.window_size_multiplier
00084: push.v self._ww
00086: pushi.e 1
00087: add.i.v
00088: pop.v.v self._ww
00090: b 00057
00091: push.v self.window_size_multiplier
00093: pushi.e 1
00094: cmp.i.v GT
00095: bf 00110
00096: pushi.e 480
00097: push.v self.window_size_multiplier
00099: mul.v.i
00100: pushi.e 640
00101: push.v self.window_size_multiplier
00103: mul.v.i
00104: call.i window_set_size(argc=2)
00106: popz.v
00107: pushi.e 1
00108: pop.v.i self.window_center_toggle
00110: call.i scr_controls_default(argc=0)
00112: popz.v
00113: call.i scr_ascii_input_names(argc=0)
00115: popz.v
00116: pushi.e 0
00117: pop.v.i self.i
00119: push.v self.i
00121: pushi.e 10
00122: cmp.i.v LT
00123: bf func_end
00124: pushi.e 0
00125: pushi.e -5
00126: push.v self.i
00128: conv.v.i
00129: pop.v.i [array]input_pressed
00131: pushi.e 0
00132: pushi.e -5
00133: push.v self.i
00135: conv.v.i
00136: pop.v.i [array]input_held
00138: pushi.e 0
00139: pushi.e -5
00140: push.v self.i
00142: conv.v.i
00143: pop.v.i [array]input_released
00145: push.v self.i
00147: pushi.e 1
00148: add.i.v
00149: pop.v.v self.i
00151: b 00119
", Data.Functions, Data.Variables, Data.Strings));

//Extra scripts for ossafe
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
00066: push.s ""argument1""@36
00068: conv.s.v
00069: ret.v
", Data.Functions, Data.Variables, Data.Strings));
Data.Code.Add(substr);
Data.CodeLocals.Add(new UndertaleCodeLocals() { Name = substr.Name });
Data.Scripts.Add(new UndertaleScript() { Name = Data.Strings.MakeString("substr"), Code = substr });
Data.Functions.EnsureDefined("substr", Data.Strings);

//Saves savedata(ofc)
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

//Loads savedata
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
00035: push.s ""argument1""@36
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

//Fills ands draws rectangles?
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
00054: push.s ""argument1""@36
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
00024: push.s ""argument1""@36
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
00048: push.s ""argument1""@36
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
00037: push.s ""argument1""@36
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

//...
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
00000: pushglb.v global.osflavor
00002: pushi.e 2
00003: cmp.i.v LTE
00004: bf 00011
00005: pushvar.v self.argument0
00007: call.i file_text_open_read(argc=1)
00009: ret.v
00010: b func_end
00011: pushvar.v self.argument0
00013: call.i string_lower(argc=1)
00015: pop.v.v local.name
00017: pushloc.v local.name
00019: pushglb.v global.savedata
00021: call.i ds_map_find_value(argc=2)
00023: pop.v.v local.file
00025: pushloc.v local.file
00027: call.i is_undefined(argc=1)
00029: conv.v.b
00030: bf 00034
00031: pushvar.v self.undefined
00033: ret.v
00034: pushloc.v local.file
00036: pop.v.v local.data
00038: pushi.e 0
00039: pop.v.i local.num_lines
00041: pushloc.v local.data
00043: call.i string_byte_length(argc=1)
00045: pushi.e 0
00046: cmp.i.v GT
00047: bf 00159
00048: pushloc.v local.data
00050: push.s ""string_byte_length""@13210
00052: conv.s.v
00053: call.i string_pos(argc=2)
00055: pop.v.v local.newline_pos
00057: pushloc.v local.newline_pos
00059: pushi.e 0
00060: cmp.i.v GT
00061: bf 00137
00062: pushloc.v local.newline_pos
00064: pushi.e 1
00065: add.i.v
00066: pop.v.v local.nextline_pos
00068: pushloc.v local.newline_pos
00070: pushi.e 1
00071: cmp.i.v GT
00072: bf 00085
00073: pushloc.v local.newline_pos
00075: pushi.e 1
00076: sub.i.v
00077: pushloc.v local.data
00079: call.i string_char_at(argc=2)
00081: push.s ""ds_map_create""@3380
00083: cmp.s.v EQ
00084: b 00086
00085: push.e 0
00086: bf 00093
00087: push.v local.newline_pos
00089: push.e 1
00090: sub.i.v
00091: pop.v.v local.newline_pos
00093: pushloc.v local.newline_pos
00095: pushi.e 1
00096: cmp.i.v GT
00097: bf 00111
00098: pushloc.v local.newline_pos
00100: pushi.e 1
00101: sub.i.v
00102: pushi.e 1
00103: conv.i.v
00104: pushloc.v local.data
00106: call.i substr(argc=3)
00108: pop.v.v local.line
00110: b 00115
00111: push.s ""argument1""@36
00113: pop.v.s local.line
00115: pushloc.v local.nextline_pos
00117: pushloc.v local.data
00119: call.i strlen(argc=1)
00121: cmp.v.v LTE
00122: bf 00132
00123: pushloc.v local.nextline_pos
00125: pushloc.v local.data
00127: call.i substr(argc=2)
00129: pop.v.v local.data
00131: b 00136
00132: push.s ""argument1""@36
00134: pop.v.s local.data
00136: b 00145
00137: pushloc.v local.data
00139: pop.v.v local.line
00141: push.s ""argument1""@36
00143: pop.v.s local.data
00145: pushloc.v local.line
00147: pushi.e -7
00148: push.v local.num_lines
00150: dup.v 0
00151: push.e 1
00152: add.i.v
00153: pop.v.v local.num_lines
00155: conv.v.i
00156: pop.v.v [array]lines
00158: b 00041
00159: call.i ds_map_create(argc=0)
00161: pop.v.v self.handle
00163: pushi.e 0
00164: conv.i.v
00165: push.s ""is_write""@13214
00167: conv.s.v
00168: push.v self.handle
00170: call.i ds_map_set(argc=3)
00172: popz.v
00173: pushloc.v local.lines
00175: push.s ""text""@13186
00177: conv.s.v
00178: push.v self.handle
00180: call.i ds_map_set(argc=3)
00182: popz.v
00183: pushloc.v local.num_lines
00185: push.s ""num_lines""@13195
00187: conv.s.v
00188: push.v self.handle
00190: call.i ds_map_set(argc=3)
00192: popz.v
00193: pushi.e 0
00194: conv.i.v
00195: push.s ""line""@9883
00197: conv.s.v
00198: push.v self.handle
00200: call.i ds_map_set(argc=3)
00202: popz.v
00203: pushi.e 0
00204: conv.i.v
00205: push.s ""line_read""@13215
00207: conv.s.v
00208: push.v self.handle
00210: call.i ds_map_set(argc=3)
00212: popz.v
00213: push.v self.handle
00215: ret.v
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
Data.Functions.EnsureDefined("ossafe_file_text_eof", Data.Strings);

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

//Save code fixing:

//Declaring the code in variables
var scr_save = Data.Scripts.ByName("scr_save")?.Code;
var scr_saveprocess = Data.Scripts.ByName("scr_saveprocess")?.Code;
var scr_saveprocess_ut = Data.Scripts.ByName("scr_saveprocess_ut")?.Code;

//custom deltarune saveprocess replacing
scr_saveprocess.Replace(Assembler.Assemble(@"
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
scr_save.Replace(Assembler.Assemble(@"
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

//Saveprocess replacement using ossafe functions TODO: fix stuff here for deltarune
scr_saveprocess_ut.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.kills
00002: pop.v.v global.lastsavedkills
00004: push.v 320.time
00006: pop.v.v global.lastsavedtime
00008: pushglb.v global.lv
00010: pop.v.v global.lastsavedlv
00012: push.s ""file""@2714
00014: pushglb.v global.filechoice
00016: call.i string(argc=1)
00018: add.v.s
00019: pop.v.v self.file
00021: push.v self.file
00023: call.i ossafe_file_text_open_write(argc=1)
00025: pop.v.v self.myfileid
00027: pushglb.v global.charname
00029: push.v self.myfileid
00031: call.i ossafe_file_text_write_string(argc=2)
00033: popz.v
00034: push.v self.myfileid
00036: call.i ossafe_file_text_writeln(argc=1)
00038: popz.v
00039: pushglb.v global.lv
00041: push.v self.myfileid
00043: call.i ossafe_file_text_write_real(argc=2)
00045: popz.v
00046: push.v self.myfileid
00048: call.i ossafe_file_text_writeln(argc=1)
00050: popz.v
00051: pushglb.v global.maxhp
00053: push.v self.myfileid
00055: call.i ossafe_file_text_write_real(argc=2)
00057: popz.v
00058: push.v self.myfileid
00060: call.i ossafe_file_text_writeln(argc=1)
00062: popz.v
00063: pushglb.v global.maxen
00065: push.v self.myfileid
00067: call.i ossafe_file_text_write_real(argc=2)
00069: popz.v
00070: push.v self.myfileid
00072: call.i ossafe_file_text_writeln(argc=1)
00074: popz.v
00075: pushglb.v global.at
00077: push.v self.myfileid
00079: call.i ossafe_file_text_write_real(argc=2)
00081: popz.v
00082: push.v self.myfileid
00084: call.i ossafe_file_text_writeln(argc=1)
00086: popz.v
00087: pushglb.v global.wstrength
00089: push.v self.myfileid
00091: call.i ossafe_file_text_write_real(argc=2)
00093: popz.v
00094: push.v self.myfileid
00096: call.i ossafe_file_text_writeln(argc=1)
00098: popz.v
00099: pushglb.v global.df
00101: push.v self.myfileid
00103: call.i ossafe_file_text_write_real(argc=2)
00105: popz.v
00106: push.v self.myfileid
00108: call.i ossafe_file_text_writeln(argc=1)
00110: popz.v
00111: pushglb.v global.adef
00113: push.v self.myfileid
00115: call.i ossafe_file_text_write_real(argc=2)
00117: popz.v
00118: push.v self.myfileid
00120: call.i ossafe_file_text_writeln(argc=1)
00122: popz.v
00123: pushglb.v global.sp
00125: push.v self.myfileid
00127: call.i ossafe_file_text_write_real(argc=2)
00129: popz.v
00130: push.v self.myfileid
00132: call.i ossafe_file_text_writeln(argc=1)
00134: popz.v
00135: pushglb.v global.xp
00137: push.v self.myfileid
00139: call.i ossafe_file_text_write_real(argc=2)
00141: popz.v
00142: push.v self.myfileid
00144: call.i ossafe_file_text_writeln(argc=1)
00146: popz.v
00147: pushglb.v global.gold
00149: push.v self.myfileid
00151: call.i ossafe_file_text_write_real(argc=2)
00153: popz.v
00154: push.v self.myfileid
00156: call.i ossafe_file_text_writeln(argc=1)
00158: popz.v
00159: pushglb.v global.kills
00161: push.v self.myfileid
00163: call.i ossafe_file_text_write_real(argc=2)
00165: popz.v
00166: push.v self.myfileid
00168: call.i ossafe_file_text_writeln(argc=1)
00170: popz.v
00171: pushi.e 0
00172: pop.v.i self.i
00174: push.v self.i
00176: pushi.e 8
00177: cmp.i.v LT
00178: bf 00218
00179: pushi.e -5
00180: push.v self.i
00182: conv.v.i
00183: push.v [array]item
00185: push.v self.myfileid
00187: call.i ossafe_file_text_write_real(argc=2)
00189: popz.v
00190: push.v self.myfileid
00192: call.i ossafe_file_text_writeln(argc=1)
00194: popz.v
00195: pushi.e -5
00196: push.v self.i
00198: conv.v.i
00199: push.v [array]phone
00201: push.v self.myfileid
00203: call.i ossafe_file_text_write_real(argc=2)
00205: popz.v
00206: push.v self.myfileid
00208: call.i ossafe_file_text_writeln(argc=1)
00210: popz.v
00211: push.v self.i
00213: pushi.e 1
00214: add.i.v
00215: pop.v.v self.i
00217: b 00174
00218: pushglb.v global.weapon
00220: push.v self.myfileid
00222: call.i ossafe_file_text_write_real(argc=2)
00224: popz.v
00225: push.v self.myfileid
00227: call.i ossafe_file_text_writeln(argc=1)
00229: popz.v
00230: pushglb.v global.armor
00232: push.v self.myfileid
00234: call.i ossafe_file_text_write_real(argc=2)
00236: popz.v
00237: push.v self.myfileid
00239: call.i ossafe_file_text_writeln(argc=1)
00241: popz.v
00242: pushi.e 0
00243: pop.v.i self.i
00245: push.v self.i
00247: pushi.e 512
00248: cmp.i.v LT
00249: bf 00273
00250: pushi.e -5
00251: push.v self.i
00253: conv.v.i
00254: push.v [array]flag
00256: push.v self.myfileid
00258: call.i ossafe_file_text_write_real(argc=2)
00260: popz.v
00261: push.v self.myfileid
00263: call.i ossafe_file_text_writeln(argc=1)
00265: popz.v
00266: push.v self.i
00268: pushi.e 1
00269: add.i.v
00270: pop.v.v self.i
00272: b 00245
00273: pushglb.v global.plot
00275: push.v self.myfileid
00277: call.i ossafe_file_text_write_real(argc=2)
00279: popz.v
00280: push.v self.myfileid
00282: call.i ossafe_file_text_writeln(argc=1)
00284: popz.v
00285: pushi.e 0
00286: pop.v.i self.i
00288: push.v self.i
00290: pushi.e 3
00291: cmp.i.v LT
00292: bf 00316
00293: pushi.e -5
00294: push.v self.i
00296: conv.v.i
00297: push.v [array]menuchoice
00299: push.v self.myfileid
00301: call.i ossafe_file_text_write_real(argc=2)
00303: popz.v
00304: push.v self.myfileid
00306: call.i ossafe_file_text_writeln(argc=1)
00308: popz.v
00309: push.v self.i
00311: pushi.e 1
00312: add.i.v
00313: pop.v.v self.i
00315: b 00288
00316: pushglb.v global.currentsong
00318: push.v self.myfileid
00320: call.i ossafe_file_text_write_real(argc=2)
00322: popz.v
00323: push.v self.myfileid
00325: call.i ossafe_file_text_writeln(argc=1)
00327: popz.v
00328: pushglb.v global.currentroom
00330: push.v self.myfileid
00332: call.i ossafe_file_text_write_real(argc=2)
00334: popz.v
00335: push.v self.myfileid
00337: call.i ossafe_file_text_writeln(argc=1)
00339: popz.v
00340: push.v 320.time
00342: push.v self.myfileid
00344: call.i ossafe_file_text_write_real(argc=2)
00346: popz.v
00347: push.v self.myfileid
00349: call.i ossafe_file_text_close(argc=1)
00351: popz.v
", Data.Functions, Data.Variables, Data.Strings));

ScriptMessage("Patched!");
