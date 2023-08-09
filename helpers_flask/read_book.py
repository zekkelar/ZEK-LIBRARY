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


import database.books as books
import helpers.controller as keys

url = keys.URL


class this_read_book(Resource):
	def __init__(self):
		self._database_books = books.this_books()

	def get_all_book(self):
		data = self._database_books.read_books()
		return jsonify({"data":data})

	def get_by_name(self, name):
		totaly = f"http://"+url+"/img/"+name
		data = self._database_books.read_by_name(totaly)
		return jsonify({"data":data})

	def get_book_uploaded(self, username):
		data = self._database_books.get_book_uploaded_byusername(username)
		return jsonify({"data":data})

	def get_views(self, name):
		totaly = f"http://"+url+"/img/"+name
		data = self._database_books.get_views(totaly)
		return jsonify({'views':data})

	def update_views(self, name, views):
		totaly = f"http://"+url+"/img/"+name
		update = self._database_books.update_views(totaly, views)
		if update:
			return jsonify({"update":"Success"})
		else:
			return jsonify({"update":"Failed"})


	def get(self):
		id = request.args.get('id')
		if id == 'all_books':
			return self.get_all_book()

		if id == 'get_by_name':
			name = request.args.get('name')
			return self.get_by_name(name)

		if id == 'get_book_uploaded':
			username = request.args.get('username')
			return self.get_book_uploaded(username)

		if id == 'get_views':
			name = request.args.get('name')
			return self.get_views(name)

		if id == 'update_views':
			name = request.args.get('name')
			views = request.args.get('views')
			return self.update_views(name, views)