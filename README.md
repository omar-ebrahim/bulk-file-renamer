# bulk-file-renamer

Simple way to bulk rename files from different directories and copy them to a specified output directory.

## Usage

Run the executable
1. Enter directory paths and press ENTER until you're done (press Y)
2. Enter a path to copy the renamed files to
3. Choose a filename to use for the output files

## Limitations in version 1.0.0

- This will copy **all** files from the specified input directories to the specified output directory
- Paths such as `\\serverfoldername` are treated as root directories, `\\serverfoldername\subfoldername` is valid
