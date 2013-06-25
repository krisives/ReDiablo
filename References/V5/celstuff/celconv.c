/**
 * @brief  bmp 24 bpp to cel converter
 * @file   celconv.h
 * @author Joel Molin
 * @date   28-12-2002
 **/

#include <stdlib.h>
#include <stdio.h>
#include <malloc.h>
#include <ctype.h>
#include <string.h>

#define TITLESTRING \
	"celconv - 24-bpp-DIB to cel converter written by Joel Molin, version 0.1"

#define USAGESTRING \
	"usage:\n" \
	"\t  celconv [%<command>] [#<index>] [-<color>] <filename>" \
	"for further info see the readme (celconv.txt)"

typedef enum {
	ADD,
	REMOVE,
	CREATE
} command_t;

typedef enum {
	false = 0,
	true
} bool;

typedef unsigned char byte;
typedef unsigned short word;
typedef unsigned long dword;

/* bitmap structures */
typedef struct {
    word    type;
    dword   size;
    word    reserved1;
    word    reserved2;
    dword   OffBits;
	dword   sizeOfStruct; /*start of the struct to count the size of */
    dword   width; /*size should be 40d*/
    dword   height;
    word    planes;
    word    bitCount;
    dword   compression;
    dword   sizeImage;
    long    XPelsPerMeter;
    long    YPelsPerMeter;
    dword   clrUsed;
    dword   clrImportant; /*end of struct*/
} bmpfileheader_t;

typedef struct {
	byte b;
	byte g;
	byte r;
	byte a;
} rgb_t;

typedef struct {
	byte* data;
	dword dataused;
	dword datasize;
} celframedata_t;

/** 
 * Turns a hex string of 6 digits to a color
 * in the form CRGB where C is a constant,
 * aa. There was an error if C is not aa.
 * THIS DO NO LONGER APPLY!
 **/
bool tocolor(const char* arg, byte* result);
celframedata_t* getcelframefrombmp(const char* filename, bool haskey, byte colorkey);

int main(int argc, char* argv[]) {

	int currentarg = 1;
	command_t command = CREATE;
	long index = 0;
	byte key = 0;
	bool haskey = false;
	char* filename;

	puts(TITLESTRING);
	if(argc == 1) {
		puts(USAGESTRING);
		return 0;
	}

	if(argc > 5) {
		puts("celconv takes 4 arguments at maximum.");
	}

	/* first check for a command */
	if(*argv[currentarg] == '%') {
		/* there is one */
		char* thecommand = argv[currentarg]+1;
		if(!strcmp(thecommand, "add")) {
			command = ADD;
		}else if(!strcmp(thecommand, "remove")) {
			command = REMOVE;
		}else if(!strcmp(thecommand, "create")) {
			command = CREATE; /*default but wth*/
		}else{
			fprintf(stderr, "error: the command '%s' is invalid", thecommand);
			return 0;
		}
		currentarg++; /*we used this arg*/
		printf("Read command as '%s'\n", thecommand);
	}
	if(currentarg > argc) {
		puts("missing necessary argument filename");
		return 0;
	}
	
	/* remove must have the index and it is optional to add */
	if(*argv[currentarg] == '#') {
		char* strindex = argv[currentarg]+1;
		index = atoi(strindex);
		currentarg++;
		printf("Read index as '%d'\n", index);
	}else if(command == REMOVE) {
		fprintf(stderr, "error: the remove command requires the index argument.");
		return 0;
	}
	if(currentarg > argc) {
		puts("missing necessary argument filename");
		return 0;
	}
	
	if('-' == *argv[currentarg]) {
		haskey = tocolor(argv[currentarg]+1, &key);
		currentarg++;
		if(haskey) {
			printf("Read colorkey as '%2X'\n", (int)key);
		}
	}
	if(currentarg > argc) {
		puts("missing necessary argument filename");
		return 0;
	}
	if(!haskey) {
		printf("No colorkey used\n");
	}

	filename = argv[currentarg];
	printf("Filename read as '%s' (currentarg=%d)\n", filename, currentarg);

	if(command == CREATE) {
		dword frames = 1;
		dword framestart = 12;
		dword fileend;
		FILE* f;
		celframedata_t* cfd = getcelframefrombmp(filename, haskey, key);

		if(!cfd) {
			puts("could not create cel");
			return 0;
		}

		f = fopen("output.cel", "wb");
		fwrite(&frames, 1, 4, f);
		fwrite(&framestart, 1, 4, f);
		fileend = 12+cfd->dataused;
		fwrite(&fileend, 1, 4, f);
		fwrite(cfd->data, 1, cfd->dataused, f);
		free(cfd->data);
		free(cfd);
	}else{
		puts("command not yet implemented");
	}
	return 0;
}

bool tocolor(const char* arg, byte* result) {
	const char* iter;
	char* endptr = NULL;
	long color;

	if(2 != strlen(arg)) {
		return false;
	}
	iter = arg;
	while(*iter) {
		if(!isxdigit(*iter)) {
			return false;
		}
		iter++;
	}
	errno = 0;
	color = strtol(arg, &endptr, 16);
	if(errno != 0) {
		perror("in tocolor (developers note to himself)");
		return false;
	}
	if(endptr == arg) {
		return false;
	}
	*result = (byte)color;
	return true;
}

celframedata_t* getcelframefrombmp(const char* filename, bool haskey, byte key) {
	byte* buffer;
	FILE* bmp;
	long filesize;
	bmpfileheader_t* bmpheader;
	byte* pixels;
	dword y;
	dword x;
	celframedata_t* cfd;
	dword colorsused;
	dword bmpx;
	dword bmpy;
	FILE* log;


	cfd = malloc(sizeof(celframedata_t));
	cfd->data = NULL;
	cfd->datasize = cfd->dataused = 0;

	bmp = fopen(filename, "rb");
	if(!bmp) {
		fprintf(stderr, "could not open bitmap file");
	}
	fseek(bmp, 0, SEEK_END);
	filesize = ftell(bmp);
	fseek(bmp, 0, SEEK_SET);
	
	buffer = malloc(filesize);
	if(!buffer) {
		puts("error: memory allocation failure");
		return NULL;
	}
	fread(buffer, 1, filesize, bmp);
	fclose(bmp);
	if(*buffer != 'B' || *(buffer+1) != 'M') {
		fputs("error: not a valid DIB (signature mismatch).", stderr);
		free(buffer); return NULL;
	}
	bmpheader = (bmpfileheader_t*)buffer;
	printf("%d\n", sizeof(bmpfileheader_t));
	if(*((dword*)(buffer+14)) != 40) {
		fputs("error: invalid DIB (must use a BITMAPINFOHEADER)", stderr);
		free(buffer); return NULL;
	}
	if(*(buffer+30)) {
		fputs("error: the DIB was compressed in some way.", stderr);
		free(buffer); return NULL;
	}
	if(*(buffer+28) != 8) {
		fputs("error: the DIB hasn't got a bit depth of 8.", stderr);
		free(buffer); return NULL;
	}
	colorsused = *(buffer+46);
	if(!colorsused) {
		colorsused = 256;
	}
	pixels = (byte*)(buffer + 54+colorsused*4);

	bmpx = *((dword*)(buffer+18));
	bmpy = *((dword*)(buffer+22));

	// the size of the bmp should be more than sufficient for our cel
	cfd->data     = malloc(filesize);
	cfd->datasize = filesize;

	log = fopen("log.txt", "wt");
	// encode line by line
	for(y = 0; y < bmpy; y++) {
		byte record;
		byte* celdata;
		
		celdata = cfd->data + cfd->dataused;

		x = 0;
		while(x < bmpx) {
			record = 0;
			if(cfd->dataused == cfd->datasize) {
				cfd->datasize *= 2;
				cfd->data = realloc(cfd->data, cfd->datasize);
				celdata = cfd->data + cfd->dataused;
			}
			if(haskey && key == *pixels) {
				while(haskey && key == *pixels &&
					record != 128 && x < bmpx)
				{
					x++;
					pixels++;
					record++;
				}
				*celdata = 0x100 - record;
				celdata++;
				cfd->dataused++; // transparency always uses 1 byte weee =)
				fprintf(log, "Writing a transparent record %d pixels wide (using %d)\n", record, 0x100-record);
			}else{
				byte* recordstartpixel;

				recordstartpixel = pixels;
				while(record < 127 && x < bmpx) {
					if(haskey && key == *pixels) {
						break;
					}
					record++;
					x++;
					pixels++;
				}
				if(cfd->dataused + record + 1 > cfd->datasize) { // +1 is for the width byte
					cfd->datasize *= 2;
					cfd->data = realloc(cfd->data, cfd->datasize);
					celdata = cfd->data + cfd->dataused;
				}
				fprintf(log, "Writing an opaque record %d bytes long\n", record);
				*celdata = record;
				celdata++;
				memcpy(celdata, recordstartpixel, record);
				celdata+=record;
				cfd->dataused += record+1;
			}
		}
	}
	fclose(log);
	free(buffer);
	return cfd;
}

bool clrcmp(rgb_t* c1, rgb_t* c2) {
	if(c1 == NULL || c2 == NULL) {
		return false;
	}
	return 
		c1->r == c2->r &&
		c1->g == c2->g &&
		c1->b == c2->b &&
		c1->a == 0xAA &&
		c2->a == 0xAA;
}

bool iscolor(rgb_t clr) {
	return clr.a == 0xAA;
}