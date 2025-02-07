import os
import re
import logging

# Configure logging to write in file logs/apps.log
logging.basicConfig(filename='logs/apps.log',level=logging.INFO, format='%(asctime)s - %(levelname)s - %(message)s')

def updateFile(filePath, pattern, replacement):
    "Update the file by replacing a pattern with a replacement."
    try:
        if not os.path.exists(filePath):
            logging.error(f"File not found: {filePath}")
            return

        with open(filePath, 'r') as file:
            content = file.read()

        updatedContent = re.sub(pattern, replacement, content)

        with open(filePath, 'w') as file:
            file.write(updatedContent)

        logging.info(f"Successfully updated {filePath}")
    except Exception as e:
        logging.error(f"Failed to update {filePath}: {e}")

def updateSConstruct(sourcePath, buildNum):
    "Update Point in the SConstruct file with BuildNum"
    sconstructPath = os.path.join(sourcePath, "develop", "global", "src", "SConstruct")
    pattern = r"point=\d+"
    replacement = f"point={buildNum}"

    updateFile(sconstructPath, pattern, replacement)

def updateVersion(sourcePath, buildNum):
    "Update ADLMSDK_VERSION_POINT in the VERSION file with BuildNum"
    versionFilePath = os.path.join(sourcePath, "develop", "global", "src", "VERSION")
    pattern = r"ADLMSDK_VERSION_POINT=\d+"
    replacement = f"ADLMSDK_VERSION_POINT={buildNum}"

    updateFile(versionFilePath, pattern, replacement)

def main():
    # Get sourcePath and buildNum from environment variables
    sourcePath = os.environ.get("SourcePath")
    buildNum = os.environ.get("BuildNum")

    if not sourcePath or not buildNum:
        logging.error("SourcePath or BuildNum environment variable's missing.")
        return

    # Perform update on both SConstruct and VERSION files
    updateSConstruct(sourcePath, buildNum)
    updateVersion(sourcePath, buildNum)

if __name__ == "__main__":
    main()

