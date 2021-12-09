# Valheim.ThisGoesHere

Small over-engineered BepInEx mod to help move files around using yaml configs.

Intended to help mod authors, by letting them supply a config file with their desired file movements.

Note, only files and paths within the BepInEx folder are valid.

ThisGoesHere scans the `BepInEx/configs` folder for any and all files named `Valheim.ThisGoesHere.yml` or with a wildcard part to add your own personal touch `Valheim.ThisGoesHere.*.yml`.

## Currently supported operations
- File
  - Copy
  - Move
  - Delete

## Format

All fields are optional.

```yml
PrintComment: #Text to print before running operations.
CopyFile:
- From: #Path to file that should be copied.
  To: #Path to file destination. If exists, target is overwriten.
MoveFile:
- From: #Path to file that should be moved
  To: #Path to file destination.
DeleteFile:
- #Path to file that should be deleted.

```
Config files are run in whatever order they are found.

Operations in each config are run in the order:
1. PrintComment
2. CopyFile
3. MoveFile
4. DeleteFile


## Example

```yaml
PrintComment: This text is printed when config is executed.
CopyFile:
- From: config/copy_this_file.txt
  To: plugins/folder/to_this_file.txt
MoveFile:
- From: config/move_this_file.txt
  To: config/to_this_folder/move_this_file.txt
- From: config/move_this_too.txt
  To: config/to_this_folder/move_this_too.txt
DeleteFile:
- config\remove_this.txt
- plugins/and_this.txt
```

This example will result in the following log when starting Valheim.

```log
[Info   :   BepInEx] Loading [This Goes Here 1.0.0]
[Debug  :This Goes Here] Found 1 config files.
[Message:This Goes Here] This text is printed when config is executed.
[Message:This Goes Here] Copying 'config\copy_this_file.txt' to 'plugins\folder\to_this_file.txt'
[Info   :This Goes Here] Creating missing folders in path.
[Message:This Goes Here] Moving 'config\move_this_file.txt' to 'config\to_this_folder\move_this_file.txt'
[Info   :This Goes Here] Creating missing folders in path.
[Message:This Goes Here] Moving 'config\move_this_too.txt' to 'config\to_this_folder\move_this_too.txt'
[Message:This Goes Here] Deleting file 'config\remove_this.txt'
[Message:This Goes Here] Deleting file 'plugins\and_this.txt'
```

# Changelog:
- v1.1.0
  - Added support for customizable file names.
- v1.0.0
  - Initial release