import requests
import json



class this_search_texts:
	def __init__(self):
		self.headers = {
		    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0',
		    'Accept': '*/*',
		    'Accept-Language': 'en-US,en;q=0.5',
		    'Referer': 'https://archive.org/details/texts?tab=collection&query=fuzzy+logic&page=2',
		    'Connection': 'keep-alive',
		    'Sec-Fetch-Dest': 'empty',
		    'Sec-Fetch-Mode': 'cors',
		    'Sec-Fetch-Site': 'same-origin',
		}
	def grab(self, page, query):
		params = {
		    'service_backend': 'metadata',
		    'user_query': query,
		    'page_type': 'collection_details',
		    'page_target': 'texts',
		    'hits_per_page': '12',
		    'page': page,
		    'aggregations': 'false',
		    'uid': 'R:1bd7bcdebe150b3bf357-S:d5989fa20d9a2371b8e1-P:2-K:h-T:1690639265483',
		    'client_url': 'https://archive.org/details/texts?tab=collection&query=fuzzy+logic&page=2',
		}

		response = requests.get(
		    'https://archive.org/services/search/beta/page_production/',
		    params=params,
		    headers=self.headers,
		)
		dapet = (self.parser(response.text))   
		if len(dapet[0]) > 0:
			return dapet 
		else:
			return False

	def parser(self, source):
		lods = json.loads(source)
		total_journal = lods['response']['body']['hits']['total']
		loads_journal = lods['response']['body']['hits']['hits']
		return loads_journal, total_journal

