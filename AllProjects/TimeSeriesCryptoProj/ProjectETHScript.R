library(fpp2)
library(tseries)
library(marima)

DataTable <- na.omit(read.csv("AllCoinHourly.csv", header = TRUE))
colnames(DataTable) <- c("Time", "BTC", "ETH", "LTC", "XRP")
head(DataTable)
tail(DataTable)

DataTable$BTC <- log(DataTable$BTC)
DataTable$LTC <- log(DataTable$LTC)
DataTable$ETH <- log(DataTable$ETH)
DataTable$XRP <- log(DataTable$XRP)

par(mfrow=c(2,2))
plot(DataTable$BTC, type = 'l', col = 'black', main = "Bitcoin")
plot(DataTable$ETH, type = 'l', col = 'red', main = "Ethereum")
plot(DataTable$LTC, type = 'l', col = 'blue', main = "Litecoin")
plot(DataTable$XRP, type = 'l', col = 'green', main = "Ripple")

acf(DataTable$BTC, lag.max = 2000)
adf.test(DataTable$BTC)

acf(DataTable$ETH, lag.max = 2000)
adf.test(DataTable$ETH)

acf(DataTable$LTC, lag.max = 2000)
adf.test(DataTable$LTC)

acf(DataTable$XRP, lag.max = 2000)
adf.test(DataTable$XRP)

# fitting vs. validation data
fitsize = .85*dim(DataTable)[1]
valsize = dim(DataTable)[1] - fitsize
fitData <- DataTable[1:fitsize,]
head(fitData)
tail(fitData)
valData <- na.omit(DataTable[fitsize+1:valsize,])
head(valData)
tail(valData)

par(mfrow = c(2,2))
#Bitcoin
BTCarima <- auto.arima(fitData$BTC)
BTCarima
plot(forecast(BTCarima$fitted, h = valsize), main = "Bitcoin Forecasts from ARIMA(2,1,3)")
points(DataTable$BTC, pch = 20, col = 'red')
acf(BTCarima$residuals)

#LiteCoin
LTCarima <- auto.arima(fitData$LTC)
LTCarima
plot(forecast(LTCarima$fitted, h = valsize), main = "Litecoin Forecasts from ARIMA(3,1,3)")
points(DataTable$LTC, pch = 20, col = 'green')
acf(LTCarima$residuals)

#Ethereum
ETHarima <- auto.arima(fitData$ETH)
ETHarima
plot(forecast(ETHarima$fitted, h = valsize), main = "Ethereum Forecasts from ARIMA(2,1,2)")
points(DataTable$ETH, pch = 20, col = 'blue')
acf(ETHarima$residuals)

#Ripple
XRParima <- auto.arima(fitData$XRP)
XRParima
plot(forecast(XRParima$fitted, h = valsize), main = "Ripple Forecasts from ARIMA(0,1,3)")
points(DataTable$XRP, pch = 20, col = 'orange')
acf(XRParima$residuals)

##Try to plot based on xreg of other coins
##############
####Bitcoin###
##############

BTC_lag1 <- na.omit(read.csv("AllCoin_BTCLag.csv", header = TRUE))
BTC_lag1 <- BTC_lag1[-1,]

BTC_lag1$BTC <- log(BTC_lag1$BTC)
BTC_lag1$LTC <- log(BTC_lag1$LTC)
BTC_lag1$ETH <- log(BTC_lag1$ETH)
BTC_lag1$XRP <- log(BTC_lag1$XRP)

#Split into train and test
fitsize = .85*dim(BTC_lag1)[1]
valsize = dim(BTC_lag1)[1] - fitsize
fitBTC <- BTC_lag1[1:fitsize,]
valBTC <- na.omit(BTC_lag1[fitsize+1:valsize,])

#Ethereum
BTC_time_series <- ts(fitBTC$BTC)
resultBTC_ETH <- auto.arima(BTC_time_series, xreg = fitData$ETH)
resultBTC_ETH
plot(forecast(resultBTC_ETH$fitted, h = valsize))
points(BTC_lag1$BTC, pch = 20, col = 'green')
#Litecoin
resultBTC_LTC <- auto.arima(BTC_time_series, xreg = fitData$LTC)
resultBTC_LTC
plot(forecast(resultBTC_LTC$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')
#Ripple
resultBTC_XRP <- auto.arima(BTC_time_series, xreg = fitData$XRP)
resultBTC_XRP
plot(forecast(resultBTC_XRP$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')
#ETH + LTC
resultBTC <- auto.arima(BTC_time_series, xreg = fitData$BTC)
resultBTC
plot(forecast(resultBTC$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')
#ETH + XRP
resultBTC <- auto.arima(BTC_time_series, xreg = fitData$BTC)
resultBTC
plot(forecast(resultBTC$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')
#LTC + XRP
resultBTC <- auto.arima(BTC_time_series, xreg = fitData$BTC)
resultBTC
plot(forecast(resultBTC$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')
#All
resultBTC <- auto.arima(BTC_time_series, xreg = fitData$BTC)
resultBTC
plot(forecast(resultBTC$fitted, h = valsize))
points(DataTable$BTC, pch = 20, col = 'green')

#All permutations were simply substituted in above

