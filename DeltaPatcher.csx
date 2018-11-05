// Fixes the inverted up and down directions, messes some collisions and theorically fixes tha saveprocess script (not yet)

EnsureDataLoaded();

ScriptMessage("Patcher for DELTARUNE on Nintendo Switch ;D");

var up_p = Data.Scripts.ByName("up_p")?.Code;
var up_h = Data.Scripts.ByName("up_h")?.Code;
var down_p = Data.Scripts.ByName("down_p")?.Code;
var down_h = Data.Scripts.ByName("down_h")?.Code;
var scr_saveprocess_ut = Data.Scripts.ByName("scr_saveprocess_ut")?.Code;

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

ScriptMessage("Patched!");
