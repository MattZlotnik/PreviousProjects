library(plyr)


#Import all data and merge into one large data frame

Tennis1 <- read.csv("AusOpen-men-2013.csv", header=TRUE, sep=",")
Tennis2 <- read.csv("FrenchOpen-men-2013.csv", header=TRUE, sep=",")
Tennis3 <- read.csv("USOpen-men-2013.csv", header=TRUE, sep=",")
Tennis4 <- read.csv("Wimbledon-men-2013.csv", header=TRUE, sep=",")

Tennis12 = rbind.fill(Tennis1, Tennis2)
Tennis123 = rbind.fill(Tennis12, Tennis3)
Tennis = rbind.fill(Tennis123, Tennis4)
View(Tennis)
attach(Tennis)
###################################
#####Exploratory Data Analysis#####
###################################

#One-on-one data exploration
par(mfrow = c(2, 5))
boxplot(FSP.1 ~ Result, xlab = "Result", ylab = "First Serve Precentage")
boxplot(ACE.1 ~ Result, xlab = "Result", ylab = "Number of Aces")
boxplot(DBF.1 ~ Result, xlab = "Result", ylab = "Double Faults")
boxplot(DBF.2 ~ Result, xlab = "Result", ylab = "Double Faults (Player 2)")
boxplot(BPC.1 ~ Result, xlab = "Result", ylab = "Break Points Created")
boxplot(BPW.1 ~ Result, xlab = "Result", ylab = "Break Points Won")
boxplot(NPA.1 ~ Result, xlab = "Result", ylab = "Net Points Attempted")
boxplot(NPW.1 ~ Result, xlab = "Result", ylab = "Net Points Won")
boxplot(UFE.1 ~ Result, xlab = "Result", ylab = "Unforced Errors")
boxplot(UFE.2 ~ Result, xlab = "Result", ylab = "Unforced Errors (Player 2)")

#Histograms of predictors
par(mfrow = c(2,5))
hist(FSP.1)
hist(ACE.1)
hist(DBF.1)
hist(DBF.2)
hist(BPC.1)
hist(BPW.1)
hist(NPA.1)
hist(NPW.1)
hist(UFE.1)
hist(UFE.2)



#Create break point percentage column
par(mfrow = c(1,1))
Tennis$BPP.1 <- Tennis$BPW.1/(Tennis$BPC.1+Tennis$BPC.2)*100
View(Tennis)
boxplot(Tennis$BPP.1 ~ Result, xlab = "Result", ylab = "Break Point Percentage (Player 1)")

#######################################
#######Model Training and Testing######
#######################################


#Split into train and test sets
set.seed(101)
TenTrain <- sample(nrow(Tennis), 2*nrow(Tennis)/3)
TennisTrain <- Tennis[TenTrain, ]
TennisTest <- Tennis[-TenTrain, ]

# Test a logistic regression to see test error rate. Predictors chosen by all possible positive things player 1
# could do to win, and add the unforced errors of player 2(UFE.2)
Logistic <- glm(Result ~ FSP.1 + ACE.1 + DBF.1 + DBF.2 + NPA.1 + NPW.1 + UFE.1 + UFE.2, data = TennisTrain, family = "binomial")
LogisticProbs <- predict(Logistic, newdata = TennisTest, type = "response")
LogisticPred <- ifelse(LogisticProbs > 0.5, 1, 0)
table(TennisTest$Result, LogisticPred)
LogisticTestError <- 1-((39+45)/(39+45+18+13))
LogisticTestError
anova(Logistic)
Logistic


# Test a tree model
library(ISLR)
library(tree)
library(rpart)

set.seed(101)

#Clean Tree. Code for initial faulty tree was inadvertantly deleted
PruneResult <- ifelse(Tennis$Result == 1, "Yes", "No")
PruneReady <- data.frame(Tennis, PruneResult)
TennisTree <- rpart(Result ~ FSP.1 + ACE.1 + DBF.1 + DBF.2 + NPA.1 + NPW.1 + UFE.1 + UFE.2, method = "class", data = PruneReady)

par(mfrow = c(1,1))
summary(TennisTree)
plot(TennisTree)
text(TennisTree, pretty = 0)






