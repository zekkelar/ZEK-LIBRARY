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



import helpers.controller as keys
import helpers_flask.account as account
import helpers_flask.archieve_journal as archieve_journal
import helpers_flask.bookmark as bookmark
import helpers_flask.download as download 
import helpers_flask.read_book as read_book 
import helpers_flask.search as search 
import helpers_flask.upload_book as upload_book


app = Flask(__name__)
api = Api(app)
CORS(app)

app.config['UPLOADED_PHOTOS_DEST'] = 'IMAGES' 
photos = UploadSet('photos', IMAGES)
configure_uploads(app, photos)



api.add_resource(upload_book.this_upload_book, "/upload_books", methods=['POST'])
api.add_resource(read_book.this_read_book, "/read_books", methods=['GET'])
api.add_resource(archieve_journal.this_archieve_journal, "/archieve_journal", methods=['GET'])
api.add_resource(account.this_account, "/account", methods=['POST'])
api.add_resource(bookmark.this_bookmark, '/bookmark', methods=['POST'])
api.add_resource(download.this_download, '/download', methods=['POST'])
api.add_resource(search.this_search, '/search', methods=['GET'])
def banner():
	print("""
███████ ███████ ██   ██ ██      ██ ██████  
   ███  ██      ██  ██  ██      ██ ██   ██ 
  ███   █████   █████   ██      ██ ██████  
 ███    ██      ██  ██  ██      ██ ██   ██ 
███████ ███████ ██   ██ ███████ ██ ██████  
                                           
[+] BackEND for ZEK-LIBRARY
[+] Coded by Zekkel AR
[+] Contact me : zekkel@zekkelar.gg | serizawa@familyattackcyber.net
	""")
if __name__ == "__main__":
	banner()
	app.run(host='0.0.0.0', port=5000, debug=True)