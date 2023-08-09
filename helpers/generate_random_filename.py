import requests
import os
import sys
from flask import Flask, jsonify
from flask_restful import Resource, Api, reqparse
from flask import Flask, render_template, request
from flask_uploads import UploadSet, configure_uploads, IMAGES
import uuid
from flask_cors import CORS
from werkzeug.utils import secure_filename

def generate(filename):
	ext = os.path.splitext(filename)[1]
	random_name = str(uuid.uuid4())
	return random_name + ext
