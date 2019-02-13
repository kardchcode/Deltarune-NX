# Deltarune-NX

Well, since now deltarune is going to be officially released by Toby Fox on the 28th of February for the switch
I will not continue over the development, instead I will probably figure out how did he ported the game and with the reference will try to write a good guide to port any gamemaker game to the switch (hopefully).

Anyway, if you still want to use this until then...
Patches Deltarune to be playable on the Nintendo Switch.

The stable version of the script (DeltaPatcherStable.csx) currently fixes collisions and controls.

The unstable version of the script (DeltaPatcherAlpha.csx) currently fucks up the game, but the main proposite is to fix the saveprocess so that it actually saves something, it's a... huh, work in progress.

## Patching Deltarune
>- You will first need this tool https://github.com/krzys-h/UndertaleModTool
>- Open then the deltarune ***data.win*** file with UndertaleModTool (data.win located at *C:\Program Files (x86)\SURVEY_PROGRAM)*).
>- Select **Run other script...** inside the **Scripts** section at the top.
>- Chose the previously downloaded file **DeltaPatcherStable.csx** from this git repo and let i run!
>- Once the opened info window says **Patched!** you can now click Accept and click **File** and then **Save** at the top.
>- Save your patched win game with the name ***game.win***
