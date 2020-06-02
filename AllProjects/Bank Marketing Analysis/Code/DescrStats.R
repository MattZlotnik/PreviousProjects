##Matt Zlotnik

Data = bank_additional_full

y = Data$y
age = Data$age
job = Data$job
MariStat = Data$marital
Educ = Data$education
Default = Data$default
HouseLoan = Data$housing
PersonLoan = Data$loan
ContType = Data$contact
LastContMon = Data$month

TotalPoints = length(y)

hist(age)
mean(age)
median(age)

?barplot
barplot(table(job), las=2)

barplot(table(MariStat), las=2)

Married <- length(which(MariStat == "married"))
Married/TotalPoints
Single <- length(which(MariStat == "single"))
Single/TotalPoints

barplot(table(Educ), las=2)
UnivDeg = length(which(Educ == "university.degree"))
UnivDeg/TotalPoints
HighSchool = length(which(Educ == "high.school"))
HighSchool/TotalPoints

barplot(table(Default), las = 2)
Def = length(which(Default == "yes"))
Def
Unkn = length(which(Default == "unknown"))
Unkn/TotalPoints

barplot(table(HouseLoan))
Yes = length(which(HouseLoan == "unknown"))
Yes/TotalPoints

barplot(table(PersonLoan), las=2)
Yes = length(which(PersonLoan == "unknown"))
Yes/TotalPoints

barplot(table(ContType), las=2)
Cell = length(which(ContType == 'cellular'))
Cell/TotalPoints

ggplot(Data$age~Data$job)
?ggplot
Age = data.frame(age)

par(barplot(table(HouseLoan)), barplot(table(ContType), las=2))

attach(mtcars)
par(mfrow = c(3,3))
barplot(table(y), main = "Y")
barplot(table(job), las=2, main = "Job")
barplot(table(MariStat), las=2, main = "Marital Status")
barplot(table(Educ), las=2, main = "Education")
barplot(table(Default), las = 2, main = "Loan Default?")
barplot(table(HouseLoan), main = "House Loan?")
barplot(table(PersonLoan), las=2, main = "Personal Loan?")
barplot(table(ContType), las=2, main = "Contact Type")
barplot(table(LastContMon), las = 2, main = "Month of Last Contact")

##, labels= c("17-30", "31-44", "45-57", "58-71", "72-84", "85-98")

bins <- cut(age, 6)
chisq.test(bins, y)
model1 <- lm(bins~y)
summary(model1)
ggplot()

chisq.test(job, y)
chisq.test(MariStat, y)
chisq.test(Educ, y)
chisq.test(Default, y)
chisq.test(HouseLoan, y)     ##Possibly not significant
chisq.test(PersonLoan, y)    ##Not significant
chisq.test(ContType, y)
chisq.test(LastContMon, y)
chisq.test(Data$emp.var.rate, y)
chisq.test(Data$cons.conf.idx, y)
chisq.test(Data$cons.price.idx, y)
chisq.test(Data$euribor3m, y)
chisq.test(durat$duration, durat$y)
chisq.test(Data$day_of_week, y)


durat <- subset(Data, duration !=0)


library(dplyr)


df <- Data %>%
  group_by(y, Data$education) %>%
  summarise(counts = n())
head(df, 20)

ggplot(df)


CutData <- subset(Data,duration != 0)


bins <- cut(CutData$age, breaks=c(0,20, 40, 60, 80, 100))
CutData$AgeBins <- bins



chisq.test(CutData$age, CutData$y)
chisq.test(CutData$job, CutData$y)
chisq.test(CutData$marital, CutData$y)
chisq.test(CutData$education, CutData$y)
chisq.test(CutData$default, CutData$y)
chisq.test(CutData$loan, CutData$y)
chisq.test(CutData$housing, CutData$y)
chisq.test(CutData$contact, CutData$y)
chisq.test(CutData$emp.var.rate, CutData$y)
chisq.test(CutData$day_of_week, CutData$y)
chisq.test(CutData$cons.conf.idx, CutData$y)
chisq.test(CutData$cons.price.idx, CutData$y)
chisq.test(CutData$euribor3m, CutData$y)



##################
##RANDOM FOREST##
##################


##Clean and remove data that will not be used

library(randomForest)
library(dplyr)
CutDat <- subset(Data,duration != 0)
CutData <- subset(CutDat,  select = -nr.employed)
CutData <- subset(CutData,  select = -housing)
CutData <- subset(CutData,  select = -loan)
CutData <- subset(CutData,  select = -duration)
CutData$y = factor(CutData$y)
CutData = CutData %>% mutate_if(is.character, as.factor)

bins <- cut(CutData$age, breaks=c(0,20, 40, 60, 80, 100))
CutData$AgeBins <- bins


##Split into train and test sets
View(CutData)
set.seed(100)
train <- sample(nrow(CutData), 0.7*nrow(CutData), replace = FALSE)
TrainSet <- CutData[train,]
ValidSet <- CutData[-train,]
summary(TrainSet)
summary(ValidSet)

##Make 3 models (Each take roughly 3 minutes to run)
model1<- randomForest(y ~ ., data = TrainSet, ntree = 500, mtry = 6, importance = TRUE)

model1

model2<- randomForest(y ~ ., data = TrainSet, ntree = 500, mtry = 10, importance = TRUE)

model2

model3<- randomForest(y ~ ., data = TrainSet, ntree = 750, mtry = 15, importance = TRUE)

model3

##Measure Success of Models
predTrain <- predict(model2, TrainSet, type = "class")
table(predTrain, TrainSet$y)

##Confusion Matrix (Sub model1, model3)
predValid <- predict(model2, ValidSet, type = "class")
mean(predValid == ValidSet$y)
table(predValid, ValidSet$y)


##Other measurements of models, both say the same thing
importance(model2)
varImpPlot(model2)
