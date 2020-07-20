import time
import csv
from BeautifulSoup import BeautifulSoup

##Define program that turns URL into a list of tables that the URL contains
def Soupify(WebAddress):
	url = WebAddress
	response = requests.get(url)
	html = response.content
	soup = BeautifulSoup(html)
	tables = soup.findAll('table')
	return tables
	
