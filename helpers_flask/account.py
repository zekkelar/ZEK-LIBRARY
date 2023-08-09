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



import database.account as account
import helpers.controller as keys

url = keys.URL

class this_account(Resource):
	def __init__(self):
		self._database_account = account.this_account()

	def isLogin(self, username, password):
		log = self._database_account.login(username, password)
		if log:
			return jsonify({"login":"Success"})
		else:
			return jsonify({"login":"Failed"})

	def register(self, username, password, machine_id):
		reg = self._database_account.register(username, password, machine_id)
		return jsonify({'status':reg})

	def machine_login(self, machine_id):
		ML = self._database_account.login_via_machine_id(machine_id)
		return jsonify({'status':ML})

	def update_profile(self, name, bio, img_url, password, username):
		UP = self._database_account.update_profile(name, bio, img_url, password, username)
		if UP:
			return jsonify({'status':'Success'})
		else:
			return jsonify({'status':'Failed'})



	def post(self):
		id =  request.form["id"]

		if id == "login":
			username = request.form["username"]
			password = request.form["password"]
			return self.isLogin(username, password)

		if id == "register":
			username = request.form["username"]
			password = request.form["password"]
			machine_id = request.form["machine_id"]
			return self.register(username, password, machine_id)

		if id == "machine_login":
			machine_id = request.form["machine_id"]
			return self.machine_login(machine_id)

		if id == "update_profile":
			name = request.form["name"]
			bio = request.form["bio"]
			password = request.form["password"]
			username = request.form["username"]


			photo_unique_filename = ""
			url_photo = request.form["url_photo"]
			if 'photo' in request.files:
				photo = request.files['photo']
				image_filename = secure_filename(photo.filename)
				unique_filename = generate_random_filename(image_filename)
				photo.save(os.path.join('C:/xampp/htdocs/img', unique_filename))
				photo_unique_filename = "http://"+url+'/img/'+unique_filename

			else:
				photo_unique_filename = url_photo

			return self.update_profile(name, bio, photo_unique_filename, password, username)