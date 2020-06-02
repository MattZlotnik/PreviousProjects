"Assigning Numerical Values to Categorical Variable"
NFL.Project.Data$CONFNUM <- ifelse(NFL.Project.Data$Conf =="AFC",0,1)

"Assigning of Variables"
y <- NFL.Project.Data$Yds
x1 <- NFL.Project.Data$Age
x2 <- NFL.Project.Data$CONFNUM
x3 <- NFL.Project.Data$Sk.


"Model and Summary"
model <- lm(y~x1+x2+x3)
model
summary(model)


"Equal Spread Condition"
RES <- resid(model)
plot(resid(model))
plot(x1,RES, xlab = "Age", ylab = "Residuals")
plot(x3,RES, xlab = "Sack Percentage", ylab = "Residuals")


"Linearity Condition"
plot(x1,y, xlab = "Age", ylab = "Yards")
plot(x3,y, xlab = "Sack%", ylab = "Yards")
plot(x1,x3)
data_test <- data.frame(x1,x2,x3)
Pred <- predict(model,data_test)
plot(Pred,RES, xlab="Predicted Yards", ylab="Residuals")


"Nearly Normal Condition"
hist(y)
hist(RES)
fivenum(y)
qqnorm(RES)

"Independence Condition"
plot(Pred,RES, xlab="Predicted Yards", ylab="Residuals")


"Confidence Interval for Coefficient of Age"
ME = SE * Tstar
SE <- 10.23
Tstar <- qt(.95,145)
ME <- SE*Tstar
49.23 - ME
49.23 + ME


"Confidence Interval for Coefficient of Conference"
ME = SE * Tstar
SE <- 81.97
Tstar <- qt(.95,145)
ME <- SE*Tstar
206.04 - ME
206.04 + ME


"Confidence Interval for Coefficient of Sack Percentage"
ME = SE * Tstar
SE <- 22.93
Tstar <- qt(.95,145)
ME <- SE*Tstar
-109.38 - ME
-109.38 + ME


"Descriptive Statistics for Yards"
sd(y)
mean(y)
var(y)
IQR(y)
range(y)


"Descriptive Statistics for Age"
sd(x1)
mean(x1)
var(x1)
IQR(x1)
range(x1)


"Descriptive Statistics for Sack Percentage"
sd(x3)
mean(x3)
var(x3)
IQR(x3)
range(x3)






