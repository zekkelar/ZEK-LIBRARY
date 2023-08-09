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



import database.download_internal as download_internal 
import database.download_external as download_external
import database.books as books
import helpers.controller as keys
url = keys.URL

class this_download(Resource):
	def __init__(self):
		self._download_external = download_external.this_download_journal_external()
		self._download_internal = download_internal.this_download_journal_internal()
		self._database_books = books.this_books()

	def isDownload_external(self,  username, source):
		IBE = self._download_external.isDownload(username, source)
		if IBE:
			return jsonify({'status':'True'})
		else:
			return jsonify({'status':'False'})

	def add_download_external(self, added_date, title, author, description, publication_date, language, source, link_download, img_url, username):
		abe = self._download_external.add_download_external(added_date, title, author, description, publication_date, language, source, link_download, img_url, username)
		if abe:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def remove_external(self, username, source):
		RE = self._download_external.remove_download(username, source)
		if RE:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def get_all_download_external(self, username):
		get = self._download_external.get_all_download(username)
		if get != False:
			return jsonify({'data':get})
		else:
			return jsonify({'data':'False'})

	def remove_all_download_external(self, username):
		get = self._download_external.remove_all(username)
		if get:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})


	def isDownload_internal(self, name, username):
		IBI = self._download_internal.isDownload(name, username)
		if IBI:
			return jsonify({'status':'True'})
		else:
			return jsonify({'status':'False'})

	def add_download_internal(self, name, username):
		ABI = self._download_internal.add_download(name, username)
		if ABI:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def remove_internal(self, name, username):
		RI = self._download_internal.remove_download(name, username)
		if RI:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def get_all_download_internal(self, username):
		get = self._download_internal.get_all_download(username)
		if get != False:
			data2 = []
			for x in get:
				totaly = f"http://"+url+"/img/"+x[1]
				data = self._database_books.read_by_name(totaly)
				data2.append(data)

			return jsonify({'data':data2})
		else:
			return jsonify({'data':'False'})

	def remove_all_download_internal(self, username):
		get = self._download_internal.remove_all(username)
		if get:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})

	def post(self):
		id = request.form['id']
		if id == 'isDownload_external':
			username = request.form["username"]
			source = request.form["source"]
			return self.isDownload_external(username, source)

		if id=='download_journal_external':
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
			return self.add_download_external(added_date, title, author, description, publication_date, language, source, link_download, img_url, username)

		if id == 'remove_download_external':
			username = request.form["username"]
			source = request.form["source"]
			return self.remove_external(username, source)

		if id=='get_all_download_external':
			username = request.form['username']
			return self.get_all_download_external(username)

		if id=='remove_all_download_external':
			username = request.form['username']
			return self.remove_all_download_external(username)


		if id=='isDownload_internal':
			username = request.form['username']
			name = request.form['name']
			return self.isDownload_internal(name, username)

		if id=='add_download_internal':
			username = request.form['username']
			name = request.form['name']
			return self.add_download_internal(name, username)

		if id=='remove_download_internal':
			username = request.form['username']
			name = request.form['name']
			return self.remove_internal(name, username)

		if id=='get_all_download_internal':
			username = request.form['username']
			return self.get_all_download_internal(username)

		if id=='remove_all_download_internal':
			username = request.form['username']
			return self.remove_all_download_internal(username)
