/* This source is part of the Cv5.x project coded in 1999 by TeLAMoN of 2XS.
   You may use this as long as proper credit is given. Algorithmic changes 
   should be reported to the author. Compiling was done with MS VC++ 5.0.
   If you want to contact the author write a mail to mickyk@cs.tu-berlin.de
   or try it per ICQ (UIN 1289212).
*/
//---------------------------------------------------------------------------
#include "CELObject.h"
//---------------------------------------------------------------------------

#include <stdio.h>
#include <io.h>
#include <fcntl.h>
#include <sys\stat.h>

// Konstruktor

CelObject::CelObject(char* IPath)
{
   AnzFrames = 0;
	Path = new MKString(IPath);
	BYTE* Buffer = LoadFile(Path->GetString());
	if (Buffer != 0)
	{
		InitCEL(Buffer);
		delete Buffer;
      	LastError = ERR_NONE;
	}
	else
		LastError = ERR_CANT_LOAD;
}

// Destruktor

CelObject::~CelObject()
{
    delete Path;
	if (AnzFrames != 0)
	{
		for (int i=0; i<AnzFrames; i++)
		{
			if (Frames[i] != 0)
         	delete Frames[i];
		}
		delete Frames;
	}
}

// Methoden (public)

// Methoden (private)

BYTE* CelObject::LoadFile(char* LPath)
{
   BYTE* Buffer = 0;
   int handle = open(LPath,O_RDONLY | O_BINARY,S_IREAD);
   if (handle != -1)
   {
		FileSize = filelength(handle);
		Buffer = new BYTE [++FileSize];
		read(handle, (void*)Buffer, --FileSize);
		close(handle);
   }
	return Buffer;
}

void CelObject::InitCEL(BYTE* Buffer)
{
  unsigned long c = 0;
  int frames      = (BYTE)Buffer[c++] ;
      frames      = frames + ((BYTE)Buffer[c++]*256);
  AnzFrames       = frames;
  FrameCEL* *NFrames = new FrameCEL* [AnzFrames];
  Frames          = NFrames;

  for (int i=0; i<AnzFrames; i++)
  {
	Frames[i] = 0;
  }

	c++;
	c++;

	//startpos der bilddaten holen
	int *filepos = new int [frames+1];

	for (int j=0; j <frames+1; j++)
	{
		filepos[j] = Buffer[c++];
		filepos[j] = filepos[j]+Buffer[c++]*256;
		filepos[j] = filepos[j]+Buffer[c++]*256*256;
		filepos[j] = filepos[j]+Buffer[c++]*256*256*256;
	}
	for (int k=0; k<frames; k++)
	{
		 Frames[k] = new FrameCEL(&Buffer[filepos[k]], (filepos[k+1]-filepos[k]));
	}
	delete filepos;
}


