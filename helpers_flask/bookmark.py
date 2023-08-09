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


import database.bookmark_external as bookmark_external
import database.bookmark_internal as bookmark_internal
import database.books as books
import helpers.controller as keys
url = keys.URL


class this_bookmark(Resource):
	def __init__(self):
		self._bookmark_external = bookmark_external.this_bookmark_external()
		self._bookmark_internal = bookmark_internal.this_bookmark_internal()
		self._database_books = books.this_books()

	def add_bookmark_external(self, added_date, title, author, description, publication_date, language, source, link_download, img_url, username):
		abe = self._bookmark_external.add_bookmark_external(added_date, title, author, description, publication_date, language, source, link_download, img_url, username)
		if abe:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def isBookmark_external(self, username, source):
		IBE = self._bookmark_external.isBookmark_external(username, source)
		if IBE:
			return jsonify({'status':'True'})
		else:
			return jsonify({'status':'False'})

	def remove_external(self, username, source):
		RE = self._bookmark_external.remove_bookmark(username, source)
		if RE:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})


	def add_bookmark_internal(self, name, username):
		ABI = self._bookmark_internal.add_bookmark(name, username)
		if ABI:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def isBookmark_internal(self, name, username):
		IBI = self._bookmark_internal.isBookmark(name, username)
		if IBI:
			return jsonify({'status':'True'})
		else:
			return jsonify({'status':'False'})


	def remove_internal(self, name, username):
		RI = self._bookmark_internal.remove_bookmark(name, username)
		if RI:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def get_all_bookmark_internal(self, username):
		get = self._bookmark_internal.get_all_bookmark(username)
		if get != False:
			data2 = []
			for x in get:
				totaly = f"http://"+url+"/img/"+x[0]
				data = self._database_books.read_by_name(totaly)
				data2.append(data)

			return jsonify({'data':data2})
		else:
			return jsonify({'data':'False'})

	def get_all_bookmark_external(self, username):
		get = self._bookmark_external.get_all_bookmark(username)
		if get != False:
			return jsonify({'data':get})
		else:
			return jsonify({'data':'False'})

	def remove_all_bookmark_internal(self, username):
		get = self._bookmark_internal.remove_all(username)
		if get:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def remove_all_bookmark_external(self, username):
		get = self._bookmark_external.remove_all(username)
		if get:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def post(self):
		id = request.form['id']
		if id=='bookmark_journal_external':
			added_date = request.form["added_date"]
			title = request.form["title"]
			author = request.form["author"]
			description = request.form["description"]
			publication_date = request.form["publication_date"]
			language = request.form["language"]
			source = request.form["source"]
			link_download = request.form["link_download"]
			img_url = request.form["img_url"]
			username = request.form["username"]
			return self.add_bookmark_external(added_date, title, author, description, publication_date, language, source, link_download, img_url, username)

		if id =='isBookmark_external':
			username = request.form["username"]
			source = request.form["source"]
			return self.isBookmark_external(username, source)

		if id == 'remove_bookmark_external':
			username = request.form["username"]
			source = request.form["source"]
			return self.remove_external(username, source)

		if id=='add_bookmark_internal':
			username = request.form['username']
			name = request.form['name']
			return self.add_bookmark_internal(name, username)

		if id=='isBookmark_internal':
			username = request.form['username']
			name = request.form['name']
			return self.isBookmark_internal(name, username)

		if id=='remove_bookmark_internal':
			username = request.form['username']
			name = request.form['name']
			return self.remove_internal(name, username)

		if id=='get_all_bookmark_internal':
			username = request.form['username']
			return self.get_all_bookmark_internal(username)

		if id=='get_all_bookmark_external':
			username = request.form['username']
			return self.get_all_bookmark_external(username)

		if id=='remove_all_bookmark_internal':
			username = request.form['username']
			return self.remove_all_bookmark_internal(username)

		if id=='remove_all_bookmark_external':
			username = request.form['username']
			return self.remove_all_bookmark_external(username)