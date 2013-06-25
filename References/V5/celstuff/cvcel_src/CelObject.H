/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#ifndef CelObject_H
#define CelObject_H
//---------------------------------------------------------------------------

#ifndef BYTE
#define BYTE unsigned char
#endif

#include "FrameCEL.h"
#include "MKString/MkString.h"

//---------------------------------------------------------------------------

#define ERR_NONE              0x0000
#define ERR_UNKNOWN_FILE_TYPE 0x0001
#define ERR_CANT_LOAD         0x0002

class CelObject
{
public:
	CelObject (char* Path);
	~CelObject ();

// Methoden
	int   GetAnzahlFrames     () {return AnzFrames;};
	char* GetPath             () {return Path->GetString();};
	long  GetFileSize         () {return FileSize;};
	int   GetLastError        () {return LastError;};
	long  GetFrameX           (int i){return Frames[i]->GetFrameX();};
	long  GetFrameY           (int i){return Frames[i]->GetFrameY();};
   void  SetFrameX    (int i, int X){Frames[i]->SetFrameX(X);};
   void  SetFrameY    (int i, int Y){Frames[i]->SetFrameY(Y);};
	BYTE* GetFrameMem         (int i){return Frames[i]->GetFrameMem();};
	unsigned long GetFrameSize(int i){return Frames[i]->GetFrameSize();};
// Daten

private:
// Methoden
	BYTE* LoadFile   (char* Path);
	void  InitCEL    (BYTE* Buffer);

// Daten
MKString*  Path;
int        AnzFrames;
long       FileSize;
FrameCEL*  *Frames;
int        LastError;
};


#endif // CelObject_H
