// DLL - Interface Defenitions
// Version 1.1
// Rev. 10/12/1999
// Implement these Interface for use with the CV 5.x
// Author: TeLAMoN of 2XS (mickyk@cs.tu-berlin.de)
// for more details check http://www.cs.tu-berlin.de/~mickyk/cv.html
// -----------------------------------------------------------------------------
#ifndef __DllShell_H
#define __DllShell_H

#ifndef BYTE
#define BYTE unsigned char
#endif

#ifndef DllEx
#define DllEx __declspec(dllexport)
#endif

/*
Chained List for saving single Frames and Anims
Convention: If ColorCount > 256 then ColorBuffer should be 0 and PixelBuffer
				contains RGB-triples ! 
*/
#ifndef DEF_SAVE_FRAME
#define DEF_SAVE_FRAME
struct SAVE_FRAME
{
	 SAVE_FRAME* NextFrame;
	 char* Path;
    BYTE* PixelBuffer;
    BYTE* ColorBuffer;
    int   dx;
    int   dy;
    int   ColorCount;
    int   BackGround;
};
#endif

DllEx char* GetDllAuthor();
DllEx char* GetDllVersion();
DllEx char* GetExtension();
DllEx char* GetDescription();

DllEx void  MoreAbout();

DllEx bool  SupportsSaveFrames();
DllEx bool  SupportsSaveAnims();
DllEx bool  LoadObject(char* Path);
DllEx bool  UnloadObject();
DllEx bool  SaveObject(SAVE_FRAME* pFrameList);
DllEx bool	SaveAnim(SAVE_FRAME* pFrameList);

DllEx int   GetNumberOfFrames();

/*
Convention: If Colors > 256 then Palette should be 0 and Buffer
				contains RGB-triples !
*/
DllEx bool  LockFrame(int Index, BYTE* *Buffer, BYTE* *Palette, int* dx, int* dy, int* Colors, int* BackGround);
DllEx bool	UnLockFrame(int Index);

#endif	//__DllShell_H
