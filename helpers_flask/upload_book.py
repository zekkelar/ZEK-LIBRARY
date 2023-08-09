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
import helpers.generate_random_filename as generate_random_filename
import database.books as books


photos = UploadSet('photos', IMAGES)

url = keys.URL

class this_upload_book(Resource):
	def __init__(self):
		self._database_books = books.this_books()

	def post(self):
		if request.method == 'POST' and 'photo' in request.files:
			title = request.form["title"]
			years = request.form["years"]
			author = request.form["author"]
			deskripsi = request.form["deskripsi"]
			username = request.form["username"]
			category = request.form["category"]
			pdf = request.files['pdf']
			photo = request.files['photo']

			image_filename = secure_filename(photo.filename)
			photo_unique_filename = generate_random_filename.generate(image_filename)

			pdf_filename = secure_filename(pdf.filename)
			pdf_unique_filename = generate_random_filename.generate(pdf_filename)

			photo.save(os.path.join('C:/xampp/htdocs/img', photo_unique_filename))
			pdf.save(os.path.join('C:/xampp/htdocs/pdf', pdf_unique_filename))
	
			self._database_books.upload_books("http://"+url+"/img/"+photo_unique_filename, author, username, title, years, deskripsi, category, url+"/pdf/"+pdf_unique_filename)
			return f'Success'
