// Fixes the inverted up and down directions, messes some collisions and... fixes tha saveprocess script?

EnsureDataLoaded();

ScriptMessage("Patcher for DELTARUNE on Nintendo Switch ;D");

//Declaring variables
var up_p = Data.Scripts.ByName("up_p")?.Code;
var up_h = Data.Scripts.ByName("up_h")?.Code;
var down_p = Data.Scripts.ByName("down_p")?.Code;
var down_h = Data.Scripts.ByName("down_h")?.Code;
var scr_saveprocess_ut = Data.Scripts.ByName("scr_saveprocess_ut")?.Code;
var ossafe_savedata_save = new UndertaleCode() { Name = Data.Strings.MakeString("gml_Script_ossafe_savedata_save") };

//Replacement using Undertale switch code as reference
up_p.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

up_h.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 0
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

down_p.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_pressed
ret.v
", Data.Functions, Data.Variables, Data.Strings));

down_h.Replace(Assembler.Assemble(@"
pushi.e -5
pushi.e 2
push.v [array]input_held
ret.v
", Data.Functions, Data.Variables, Data.Strings));

scr_saveprocess_ut.Replace(Assembler.Assemble(@"
pushglb.v global.kills
pop.v.v global.lastsavedkills
push.v 320.time
pop.v.v global.lastsavedtime
pushglb.v global.lv
pop.v.v global.lastsavedlv
push.s ""file""@2714
pushglb.v global.filechoice
call.i string(argc=1)
add.v.s
pop.v.v self.file
push.v self.file
call.i ossafe_file_text_open_write(argc=1)
pop.v.v self.myfileid
pushglb.v global.charname
push.v self.myfileid
call.i ossafe_file_text_write_string(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.lv
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.maxhp
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.maxen
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.at
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.wstrength
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.df
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.adef
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.sp
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.xp
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.gold
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.kills
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushi.e 0
pop.v.i self.i
push.v self.i
pushi.e 8
cmp.i.v LT
bf 00218
pushi.e -5
push.v self.i
conv.v.i
push.v [array]item
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushi.e -5
push.v self.i
conv.v.i
push.v [array]phone
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
push.v self.i
pushi.e 1
add.i.v
pop.v.v self.i
b 00174
pushglb.v global.weapon
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.armor
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushi.e 0
pop.v.i self.i
push.v self.i
pushi.e 512
cmp.i.v LT
bf 00273
pushi.e -5
push.v self.i
conv.v.i
push.v [array]flag
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
push.v self.i
pushi.e 1
add.i.v
pop.v.v self.i
b 00245
pushglb.v global.plot
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushi.e 0
pop.v.i self.i
push.v self.i
pushi.e 3
cmp.i.v LT
bf 00316
pushi.e -5
push.v self.i
conv.v.i
push.v [array]menuchoice
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
push.v self.i
pushi.e 1
add.i.v
pop.v.v self.i
b 00288
pushglb.v global.currentsong
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
pushglb.v global.currentroom
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_writeln(argc=1)
popz.v
push.v 320.time
push.v self.myfileid
call.i ossafe_file_text_write_real(argc=2)
popz.v
push.v self.myfileid
call.i ossafe_file_text_close(argc=1)
popz.v
", Data.Functions, Data.Variables, Data.Strings));

//Finally adding files thx to krzys-h
ossafe_savedata_save.Append(Assembler.Assemble(@"
.localvar 0 arguments
.localvar 1 json 871
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

ScriptMessage("Patched!");
