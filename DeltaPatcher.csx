// Fixes the inverted up and down directions atm, ATM

EnsureDataLoaded();

ScriptMessage("Patcher for DELTARUNE on Nintendo Switch ;D");

var up_p = Data.Scripts.ByName("up_p")?.Code;
var up_h = Data.Scripts.ByName("up_h")?.Code;
var down_p = Data.Scripts.ByName("down_p")?.Code;
var down_h = Data.Scripts.ByName("down_h")?.Code;

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


//This commented section is a debugger mode enabler, experimentally dedicated to jump to 
//the first free-moving room so I don't have to "design a vessel" every time I enter the 
//game you know...
/* 
ScriptMessage("Debug mode enabler\nby krzys_h");

var scr_debug = Data.Scripts.ByName("scr_debug")?.Code;
scr_debug.Replace(Assembler.Assemble(@"
pushglb.v global.debug
ret.v
", Data.Functions, Data.Variables, Data.Strings));
*/

ScriptMessage("Patched!");
