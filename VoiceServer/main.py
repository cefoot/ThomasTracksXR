import os
from fastapi import FastAPI
from TTS.api import TTS
from fastapi.responses import FileResponse

app = FastAPI(ssl_keyfile=os.environ['SERVER_KEYFILE'], ssl_certfile=os.environ['SERVER_CERTFILE'])
tts = TTS("tts_models/multilingual/multi-dataset/xtts_v1").to('cuda').float()

def convert_to_speech(text):
    tts.tts_to_file(
        text=text,
        file_path=os.environ['TEMP_FILE_PATH'],
        speaker_wav=os.environ['REFERENCE_SPEAKER_PATH'],
        language="de"
    )

@app.get("/askHelp/{text}")
async def generate_speech(text):
    convert_to_speech(text)
    return FileResponse(os.environ['TEMP_FILE_PATH'], media_type="audio/wav")