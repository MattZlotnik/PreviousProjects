import time
import csv
import requests
from BeautifulSoup import BeautifulSoup

##Define program that turns URL into a list of tables that the URL contains
def Soupify(WebAddress):
	url = WebAddress
	response = requests.get(url)
	html = response.content
	soup = BeautifulSoup(html)
	tables = soup.findAll('table')
	return tables

##Define program that will use the summary table to determine whether Texas is playing at home
##and determine the opponent and whether or not the opponent is in the Big 12
def HomeorAway(tables):
	global BIGXII
	global Opponent
	BIGXII = "null"
	Opponent = "null"
	
##Remove the first table as it is the only relevant one for this and becomes irrelevant after
	Summary = tables.pop(0) 
	list_of_rows = []
	for row in Summary.findAll('tr'):
		list_of_cells = []
		for cell in row.findAll('td'):
			text = cell.text
			list_of_cells.append(text)
		list_of_rows.append(list_of_cells)
		
##If Texas is listed first, Texas is away, check the home team for Big12 or not
	if list_of_rows[1][0] == "TEX":
		Opponent = list_of_rows[2][0]
		if Opponent in Big12Teams:
			BIGXII = 1
		else:
			BIGXII = 0
		return 'AWAY'
	else:

##Otherwise check the away team to see if Big12
		Opponent = list_of_rows[1][0]
		if Opponent in Big12Teams:
			BIGXII = 1
		else:
			BIGXII = 0
		return 'HOME'

def ChangePossession(Possession):
		if Possession == "Texas":
			Possession = Opponent
		elif Possession == Opponent:
			Possession = "Texas"
		return Possession





##The Big Daddy, does all major parsing of data
def GetRows(HomeAway, tables, GC, Possession):

##Deal with different numbers of tables on ESPN. Should be a max of 3
	if len(tables) > 2:
		tables.pop(-1)
	if len(tables) > 2:
		tables.pop(-1)
	if Opponent in ["UTSA", "KSU", "MICH", "TA&M;"]:
		Possession = "Texas"
	list_of_rows = []
	prev_list_of_cells = []
	
	for table in tables:
		for row in table.findAll('tr'):
			CloseGame = 0
			Year = 2015
			OFTA = 0
			OFTM = 0
			TFTA = 0
			TFTM = 0
			OLDA = 0
			OLDM = 0
			OJA = 0
			OJM = 0
			O3PA = 0
			O3PM = 0
			TLDA = 0
			TLDM = 0
			TJA = 0
			TJM = 0
			T3PA = 0
			T3PM = 0
			OTNV = 0
			TTNV = 0
			Foul = 0
			list_of_cells = [0,0,0]
			cellcount = 0
			for cell in row.findAll('td'):
				text = cell.text
				
##Check for overtime or not
				if text == "End of Game":
					tables.pop(-1)
					
##Work the data
				if cellcount == 0:
					list_of_cells[0] = text
				if cellcount == 2:
					text_list = text.lower().split()
					text_list = [str(r) for r in text_list]
					PlayerName = text_list[0] + " " + text_list[1]
					
					if "three" in text_list and "point" in text_list:
						if Possession == "Texas":
							T3PA = 1
							if "made" in text_list:
								T3PM = 1
						elif Possession == Opponent:
							O3PA = 1
							if "made" in text_list:
								O3PM = 1
					if "dunk." in text_list or "layup." in text_list or ("tip" in text_list and "shot." in text_list):
						if Possession == "Texas":
							TLDA = 1
							if "made" in text_list:
								TLDM = 1
						elif Possession == Opponent:
							OLDA = 1
							if "made" in text_list:
								OLDM = 1
				
					if "jumper." in text_list:
						if Possession == "Texas":
							TJA = 1
							if "made" in text_list:
								TJM = 1
						elif Possession == Opponent:
							OJA = 1
							if "made" in text_list:
								OJM = 1
					
					if "free" in text_list:
						if Possession == "Texas":
							TFTA = 1
							if "made" in text_list:
								TFTM = 1
						elif Possession == Opponent:
							OFTA = 1
							if "made" in text_list:
								OFTM = 1
								
					if "deadball" in PlayerName or "deadball" in text_list:
						if "texas" in PlayerName:
							Possession = "Texas"
						else:
							Possession = Opponent
					if "foul" in text_list:
						if (prev_list_of_cells[0] == list_of_cells[0]) and (("made" in prev_list_of_cells[3]) or ("made" in list_lag_2[3])):
							Possession = ChangePossession(Possession)
						Foul = 1
					else:
						Foul = 0
					if 'turnover.' in text_list:
						if Possession == "Texas":
							TTNV = 1
						elif Possession == Opponent:
							OTNV = 1

					list_of_cells[2] = PlayerName
					text_list.pop(1)
					text_list.pop(0)
					if PlayerName == "jump ball":
						if text_list[-1] == "texas":
							Possession = "Texas"
						else:
							Possession = Opponent
					if "made" in text_list:
						list_of_cells[1] = Possession
						if "throw." in text_list:
							if "made" in list_lag_2[3] and "foul" in prev_list_of_cells[2] and prev_list_of_cells[0] == list_lag_2[0]:
								Possession = ChangePossession(Possession)
							elif ("throw." in prev_list_of_cells[3]) or ("team" in prev_list_of_cells[3]):
								Possession = ChangePossession(Possession)								
						else:	
							Possession = ChangePossession(Possession)
					elif "turnover." in text_list or "turnover." in PlayerName:
						list_of_cells[1] = Possession
						Possession = ChangePossession(Possession)
					elif ("defensive" in text_list or "defensive" in PlayerName) and ("rebound." in text_list):
						list_of_cells[1] = Possession
						Possession = ChangePossession(Possession)
					else:
						list_of_cells[1] = Possession
						
						
					list_of_cells.append(text_list)
				elif cellcount == 3:
					x = text.split(" - ")
					list_of_cells.append(x[0])
					list_of_cells.append(x[1])
					Hscore = int(x[0])
					Ascore = int(x[1])
					
					if abs(Ascore - Hscore) < 10:
						CloseGame = 1
						print "It's Close!"
					else:
						CloseGame = 0
						print "Blowout"
				
				
				
				cellcount+=1
				
##Add everything
			list_of_cells.append(Foul)
			list_of_cells.append(TTNV)
			list_of_cells.append(OTNV)
			list_of_cells.append(T3PA)
			list_of_cells.append(T3PM)
			list_of_cells.append(O3PA)
			list_of_cells.append(O3PM)
			list_of_cells.append(TLDA)
			list_of_cells.append(TLDM)
			list_of_cells.append(OLDA)
			list_of_cells.append(OLDM)
			list_of_cells.append(TJA)
			list_of_cells.append(TJM)
			list_of_cells.append(OJA)
			list_of_cells.append(OJM)
			list_of_cells.append(TFTA)
			list_of_cells.append(TFTM)
			list_of_cells.append(OFTA)
			list_of_cells.append(OFTM)
			list_of_cells.append(CloseGame)
			list_of_cells.append(HomeAway)
			list_of_cells.append(BIGXII)
			list_of_cells.append(Opponent)
			list_of_cells.append(GC)
			list_of_cells.append(Year)
			if list_of_cells[0] != 0:
				list_of_rows.append(list_of_cells)				
			list_lag_2 = prev_list_of_cells
			prev_list_of_cells = list_of_cells
			
	return list_of_rows

def WriteOutput(list_of_rows):	
	outfile = open("./playbyplay.csv", "wb")
	writer = csv.writer(outfile)
	writer.writerow(["Time","Possession","Player","Text","AwayScore","HomeScore","Foul","TTNV", "OTNV", "T3PA", "T3PM", "O3PA", "O3PM", "TLDA", "TLDM","OLDA", "OLDM", "TJA", "TJM", "OJA", "OJM", "TFTA", "TFTM", "OFTA", "OFTM", "CloseGame", "Venue", "Big12","Opponent","GameNumber","Year"])
	writer.writerows(list_of_rows)





list_of_URLS_2015 = ["http://www.espn.com/mens-college-basketball/playbyplay?gameId=400818908&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816984&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400827675&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400854122&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816985&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816986&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816987&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816989&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816990&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400816808&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831506&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831508&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831511&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831514&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831334&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400830918&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831524&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831528&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400809423&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831529&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831535&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831536&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831541&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400830925&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831545&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831548&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831552&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831555&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400831346&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400870173&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400871257&wsVar=us~ncb~gamepackage,desktop,en"] 
list_of_URLS_2016 = ["http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911021&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911022&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911023&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400926735&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911024&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400910545&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911025&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911026&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400910584&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911028&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911029&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911030&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911031&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911032&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911033&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911034&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911035&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911036&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400910672&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911037&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911038&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911039&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911040&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911041&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911042&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911043&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911044&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911045&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400911046&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400945527&wsVar=us~ncb~gamepackage,desktop,en", "http://www.espn.com/mens-college-basketball/playbyplay?gameId=400945797&wsVar=us~ncb~gamepackage,desktop,en"]
list_of_URLS_2017 = 
list_of_URLS_2018 = 


global Big12Teams
Big12Teams = {'TTU', 'KSU', 'KU', 'BU', 'ISU', 'TCU', 'OSU', 'OU', 'WVU'}
DataFile = []
GameCount = 1
Possession = "Begin"
Year = 2015
for URL in list_of_URLS_2015:
	tables = Soupify(URL)
	HomeAway = HomeorAway(tables)
	DataFile += GetRows(HomeAway, tables, GameCount, Possession)
	GameCount+=1
	
WriteOutput(DataFile)