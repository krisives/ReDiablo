#include <stdio.h>
#include <malloc.h>

/* types */
typedef unsigned long dword;

/* function declarations */
int printCEL(char* filename, FILE* output);


int main(int argc, char* argv[]) {
	if(argc != 2) {
		puts("celdump: No inputfiles or confused by too many arguments.");
		return 1;
	}

	return printCEL(argv[1], stdout);

	//return 0;
}

int printCEL(char* filename, FILE* output) {
	FILE* f;
	char*  buffer;
	dword* bufferhelp;
	dword  filesize;
	dword  frames;
	dword* frameoffsets; /* the last one of these will be the fileend offset */
	int    i;

	f = fopen(filename, "rb");
	
	if(f == NULL) {
		puts("celdump: Could not open the inputfile.");
		return 1;
	}

	fseek(f, 0, SEEK_END);
	filesize = ftell(f);
	fseek(f, 0, SEEK_SET);

	buffer = malloc(filesize);
	fread(buffer, 1, filesize, f);
	fclose(f);

	bufferhelp = (dword*)buffer;

	frames = *bufferhelp;
	if(frames > 1024) {
		puts("celdump: This CEL (or whatever it is) holds more than 1024"
			" images and thus parsing will not continue.");
		free(buffer);
		return 1;
	}
	frameoffsets = malloc(sizeof(dword)*(frames+1));
	if(NULL == frameoffsets) {
		puts("celdump: Error allocating memory");
		free(buffer);
		return 1;
	}
	
	for(i = 0; i <= frames; i++) { /* <= is because we want the fileend offset too. */
		bufferhelp++;
		frameoffsets[i] = *bufferhelp;
	}

	for(i = 0; i < frames; i++) {
		char* bufferiter;
		int framesize;
		dword allwidths = 0;
		dword numwidths = 0;
		int j = 0;
		int read = 0;
		
		framesize = frameoffsets[i+1] - frameoffsets[i];

		fprintf(output, "Frame %06d info (%u bytes @ offset %u)"
			"\n----------------------------------------\n"
			"The 'width' bytes:", i, framesize, frameoffsets[i]);

		bufferiter = buffer + frameoffsets[i];
		while(read < framesize) {
			unsigned char blah = (char)*bufferiter;

			//if(blah > 127) {
		//		printf("\n\n\nread: %d\n", read);
			//	return 0;
			//}

			if(j%8==0) {
				printf("\n%08X:   ", frameoffsets[i]+read);
			}j++;
			fprintf(output, "%03u ", (int)blah);
			allwidths += blah;
			numwidths++;

			if(*bufferiter == 0) {
				puts("celdump: *bufferiter was zero, cannot continue. (infinite loop)");
				return 0;
			}

			read += blah+1;
			bufferiter += blah+1;
		}
		
		fprintf(output, "\n\nAll together the %d widths were %u, which has an average of"
			" %g,\n\n", numwidths, allwidths, (float)allwidths / numwidths);
	}

	free(buffer);
	return 0;
}