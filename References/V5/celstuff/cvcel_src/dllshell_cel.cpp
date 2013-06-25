/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#include <windows.h>
#include "dllshell.h"
#include "celobject.h"

CelObject* Object = 0;

BOOL WINAPI DllMain( HINSTANCE hinstDll,
                           DWORD fdwRreason,
                           LPVOID plvReserved)
{
    return true;   // Indicate that the DLL was initialized successfully.
}

int FAR PASCAL WEP ( int bSystemExit )
{
	 if (Object != 0)
    	delete Object;
    return 1;
}

DllEx char* GetDllAuthor()
{
	return "TeLAMoN of 2XS in 1999";
}

DllEx char* GetDllVersion()
{
	return "0.9c";
}

DllEx char* GetExtension()
{
	return "cel";
}

DllEx char* GetDescription()
{
	return "Blizzard Diablo CEL-File-Format";
}


DllEx bool SupportsSaveFrames()
{
	return false;
}

DllEx bool SupportsSaveAnims()
{
	return false;
}

DllEx bool LoadObject(char* Path)
{
   if (Object != 0)
   	delete Object;
   Object = new CelObject(Path);
   if (Object->GetLastError() == ERR_NONE)
   	return true;
   else
   {
   	delete Object;
      Object = 0;
      return false;
   }
}

DllEx int GetNumberOfFrames()
{
	if (Object != 0)
   {
   	return Object->GetAnzahlFrames();
   }
   else
   	return 0;
}

DllEx void MoreAbout()
{
}

DllEx bool LockFrame(int Index, BYTE* *Buffer, BYTE* *Palette, int* dx, int* dy, int* C, int* BG)
{
   bool ret = false;
   if (Object != 0)
   {
   	*Palette = 0;
      *C       = 256;
      *BG      = 0;
      *Buffer  = Object->GetFrameMem(Index);
      *dx      = Object->GetFrameX(Index);
      *dy      = Object->GetFrameY(Index);
      ret      = true;
   }
   return ret;
}

DllEx bool UnLockFrame(int Index)
{
	return true;
}

DllEx bool UnloadObject()
{
	bool ret = true;
   if (Object != 0)
   	delete Object;
   else
   	ret = false;
   Object = 0;
   return ret;
}

DllEx bool SaveObject(SAVE_FRAME* pFrameList)
{
	return false;
}

DllEx bool SaveAnim(SAVE_FRAME* pFrameList)
{
	return false;
}

