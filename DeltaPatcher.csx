// Fixes the inverted up and down directions, messes some collisions and... fixes tha saveprocess? v0.1

EnsureDataLoaded();

ScriptMessage("DELTARUNE patcher for the Nintendo Switch\nby Kardch\nv0.1");

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

//Declaring and creating necessary variables for code-strings-functions-whatever (extra and ossafe) TODO: LocalsCount argument for each script
var scr_saveprocess_ut = Data.Scripts.ByName("scr_saveprocess_ut")?.Code;
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

//Ensure the missing GLOBAL variables
Data.Variables.EnsureDefined("osflavor", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_id", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("switchlogin", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);  //Not necessary yet...
Data.Variables.EnsureDefined("savedata", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_buffer", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_async_load", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("savedata_debuginfo", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);
Data.Variables.EnsureDefined("current_ini", UndertaleInstruction.InstanceType.Global, false, Data.Strings, Data);

//Ensure the misssing SELF variable
Data.Variables.EnsureDefined("undefined", UndertaleInstruction.InstanceType.Self, false, Data.Strings, Data);

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

//Ensure the missing functions
Data.Functions.EnsureDefined("buffer_async_group_begin", Data.Strings);
Data.Functions.EnsureDefined("buffer_async_group_option", Data.Strings);
Data.Functions.EnsureDefined("json_encode", Data.Strings);
Data.Functions.EnsureDefined("string_byte_length", Data.Strings);
Data.Functions.EnsureDefined("buffer_create", Data.Strings);
Data.Functions.EnsureDefined("buffer_write", Data.Strings);
Data.Functions.EnsureDefined("buffer_get_size", Data.Strings);
Data.Functions.EnsureDefined("buffer_save_async", Data.Strings);
Data.Functions.EnsureDefined("buffer_async_group_end", Data.Strings);
Data.Functions.EnsureDefined("buffer_load_async", Data.Strings);
Data.Functions.EnsureDefined("string_lower", Data.Strings);
Data.Functions.EnsureDefined("is_undefined", Data.Strings);
Data.Functions.EnsureDefined("ini_open_from_string", Data.Strings);
Data.Functions.EnsureDefined("ds_map_set", Data.Strings);
Data.Functions.EnsureDefined("ds_map_set_post", Data.Strings);
Data.Functions.EnsureDefined("substr", Data.Strings);
Data.Functions.EnsureDefined("strlen", Data.Strings);
Data.Functions.EnsureDefined("ds_map_delete", Data.Strings);

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
00014: push.s ""Deltarune""@6271
00016: conv.s.v
00017: call.i buffer_async_group_begin(argc=1)
00019: popz.v
00020: pushi.e 0
00021: conv.i.v
00022: push.s ""showdialog""@6273
00024: conv.s.v
00025: call.i buffer_async_group_option(argc=2)
00027: popz.v
00028: pushi.e 0
00029: conv.i.v
00030: push.s ""savepadindex""@6275
00032: conv.s.v
00033: call.i buffer_async_group_option(argc=2)
00035: popz.v
00036: push.s ""Deltarune""@6271
00038: conv.s.v
00039: push.s ""slottitle""@6276
00041: conv.s.v
00042: call.i buffer_async_group_option(argc=2)
00044: popz.v
00045: push.s ""Deltarune Save Data""@6277
00047: conv.s.v
00048: push.s ""subtitle""@6278
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
00089: push.s ""deltarune.sav""@6281
00091: conv.s.v
00092: pushglb.v global.savedata_buffer
00094: call.i buffer_save_async(argc=4)
00096: popz.v
00097: pushi.e 0
00098: pop.v.i global.savedata_async_load
00100: push.s ""save in progress""@6292
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
00006: push.s ""Deltarune""@6271
00008: conv.s.v
00009: call.i buffer_async_group_begin(argc=1)
00011: popz.v
00012: pushi.e 0
00013: conv.i.v
00014: push.s ""showdialog""@6273
00016: conv.s.v
00017: call.i buffer_async_group_option(argc=2)
00019: popz.v
00020: pushi.e 0
00021: conv.i.v
00022: push.s ""savepadindex""@6275
00024: conv.s.v
00025: call.i buffer_async_group_option(argc=2)
00027: popz.v
00028: push.s ""Deltarune""@6271
00030: conv.s.v
00031: push.s ""slottitle""@6276
00033: conv.s.v
00034: call.i buffer_async_group_option(argc=2)
00036: popz.v
00037: push.s ""Deltarune Save Data""@6277
00039: conv.s.v
00040: push.s ""subtitle""@6278
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
00061: push.s ""deltarune.sav""@6281
00063: conv.s.v
00064: pushglb.v global.savedata_buffer
00066: call.i buffer_load_async(argc=4)
00068: popz.v
00069: pushi.e 1
00070: pop.v.i global.savedata_async_load
00072: push.s ""load in progress""@6284
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
00015: push.s ""data""@6241
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: push.s """"@6257
00024: add.s.v
00025: push.s ""data""@6241
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
00017: push.s ""data""@6241
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_find_value(argc=2)
00024: pushvar.v self.argument1
00026: add.v.v
00027: push.s ""data""@6241
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
00017: push.s ""data""@6241
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_find_value(argc=2)
00024: pushvar.v self.argument1
00026: call.i string(argc=1)
00028: add.v.v
00029: push.s ""data""@6241
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
00017: push.s ""line_read""@6253
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_set(argc=3)
00024: popz.v
00025: push.s ""line""@5066
00027: conv.s.v
00028: pushloc.v local.handle
00030: call.i ds_map_find_value(argc=2)
00032: pushi.e 1
00033: add.i.v
00034: push.s ""line""@5066
00036: conv.s.v
00037: pushloc.v local.handle
00039: call.i ds_map_set_post(argc=3)
00041: pop.v.v local.line
00043: pushloc.v local.line
00045: push.s ""num_lines""@3215
00047: conv.s.v
00048: pushloc.v local.handle
00050: call.i ds_map_find_value(argc=2)
00052: cmp.v.v GTE
00053: bf 00058
00054: push.s ""argument1""@36
00056: conv.s.v
00057: ret.v
00058: push.s ""text""@5052
00060: conv.s.v
00061: pushloc.v local.handle
00063: call.i ds_map_find_value(argc=2)
00065: pop.v.v self.text
00067: pushi.e -1
00068: pushloc.v local.line
00070: conv.v.i
00071: push.v [array]text
00073: push.s """"@6257
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
00015: push.s ""line_read""@6253
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00028
00024: push.s ""argument1""@36
00026: conv.s.v
00027: ret.v
00028: push.s ""line""@5066
00030: conv.s.v
00031: pushloc.v local.handle
00033: call.i ds_map_find_value(argc=2)
00035: pop.v.v local.line
00037: pushloc.v local.line
00039: push.s ""num_lines""@3215
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
00054: push.s ""line_read""@6253
00056: conv.s.v
00057: pushloc.v local.handle
00059: call.i ds_map_set(argc=3)
00061: popz.v
00062: push.s ""text""@5052
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
00015: push.s ""line_read""@6253
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00027
00024: pushi.e 0
00025: conv.i.v
00026: ret.v
00027: push.s ""line""@5066
00029: conv.s.v
00030: pushloc.v local.handle
00032: call.i ds_map_find_value(argc=2)
00034: pop.v.v local.line
00036: pushloc.v local.line
00038: push.s ""num_lines""@3215
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
00052: push.s ""line_read""@6253
00054: conv.s.v
00055: pushloc.v local.handle
00057: call.i ds_map_set(argc=3)
00059: popz.v
00060: push.s ""text""@5052
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
00017: push.s ""is_write""@6240
00019: conv.s.v
00020: pushloc.v local.handle
00022: call.i ds_map_set(argc=3)
00024: popz.v
00025: pushvar.v self.argument0
00027: call.i string_lower(argc=1)
00029: push.s ""filename""@6242
00031: conv.s.v
00032: pushloc.v local.handle
00034: call.i ds_map_set(argc=3)
00036: popz.v
00037: push.s ""argument1""@36
00039: conv.s.v
00040: push.s ""data""@6241
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
00050: push.s """"@6247
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
00081: push.s """"@6251
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
00165: push.s ""is_write""@6240
00167: conv.s.v
00168: push.v self.handle
00170: call.i ds_map_set(argc=3)
00172: popz.v
00173: pushloc.v local.lines
00175: push.s ""text""@5052
00177: conv.s.v
00178: push.v self.handle
00180: call.i ds_map_set(argc=3)
00182: popz.v
00183: pushloc.v local.num_lines
00185: push.s ""num_lines""@3215
00187: conv.s.v
00188: push.v self.handle
00190: call.i ds_map_set(argc=3)
00192: popz.v
00193: pushi.e 0
00194: conv.i.v
00195: push.s ""line""@5066
00197: conv.s.v
00198: push.v self.handle
00200: call.i ds_map_set(argc=3)
00202: popz.v
00203: pushi.e 0
00204: conv.i.v
00205: push.s ""line_read""@6253
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
00015: push.s ""line""@5066
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: push.s ""num_lines""@3215
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
00015: push.s ""is_write""@6240
00017: conv.s.v
00018: pushloc.v local.handle
00020: call.i ds_map_find_value(argc=2)
00022: conv.v.b
00023: bf 00043
00024: push.s ""data""@6241
00026: conv.s.v
00027: pushloc.v local.handle
00029: call.i ds_map_find_value(argc=2)
00031: push.s ""filename""@6242
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

//Saveprocess replacement using ossafe functions TODO: fix stuff here for deltarune
scr_saveprocess_ut.Replace(Assembler.Assemble(@"
.localvar 0 arguments
00000: pushglb.v global.kills
00002: pop.v.v global.lastsavedkills
00004: push.v 320.time
00006: pop.v.v global.lastsavedtime
00008: pushglb.v global.lv
00010: pop.v.v global.lastsavedlv
00012: push.s ""file""@3371
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
