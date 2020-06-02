library(mosaic)
library(MatchIt)
library(lattice)
source('http://jgscott.github.io/teaching/r/utils/class_utils.R')


model1 = lm(CS ~ Home, data = NFLData)
boxplot(CS~Home, data = NFLData, las=2)
model1
boxplot(CS~Away, data = NFLData, las=2)
boxplot(CS~OU, data=NFLData)
simple_anova(model1)
model2 = lm(CS~Dist+Home+Dist:Home, data = NFLData)
simple_anova(model2)
model2
summary(model2)

model3 = lm(APD ~ Line, data = NFLData)
model3
simple_anova(model3)
plot(APD~Line, data = NFLData)


mean(NFLWest$COU~NFLWest$TZC)
count(NFLWest$TZC==0)
hist(NFLWest$TZC, breaks = 4)

model4 = lm(Line~Dist, data = NFLData)
simple_anova((model4))
model4
summary(model4)




HomeDogs <- subset(NFLData, Line<=0)
AwayDogs <- subset(NFLData, Line > 0)


HomeDogMean <- mean(HomeDogs$CS)
HomeDogMean
AwayDogsMean <- mean(AwayDogs$CS)
AwayDogsMean


FlipHome = do(100000)*{
  nflip(count(HomeDogs))/count(HomeDogs)
}
FlipHome
hist(FlipHome$X1., xlab = "Winning Percentage", main = "Histogram of Nflip Results")
abline(v=HomeDogMean, col="Green")
pvalHome = sum(mean(HomeDogMean) >= FlipHome$X1.)/count(FlipHome)
pvalHome
count(FlipHome)


FlipAway = do(100000)*{
  nflip(count(AwayDogs))/count(AwayDogs)
}
FlipAway
hist(FlipAway$X1.)
abline(v=AwayDogsMean, col="Green")
pvalAway = sum(mean(AwayDogsMean) >= FlipAway$X1.)/count(FlipAway)
pvalAway





summary(ABHU)
plot(CS~HomeDog, data=NFLData)
mean(NFLData$CS, NFLData$HomeDog==YES)
ABHU
simple_anova(ABHU)


summary(boot2)
boot2















