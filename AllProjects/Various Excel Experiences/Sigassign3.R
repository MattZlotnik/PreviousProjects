nbanew=NBA_2013_to_2014_SW[2:36]
nbaoff=nbanew[c("A_OFF", "A_TO", "FGP", "X3pp", "FTP", "MPG")]
kcoff = kmeans(nbaoff, 5)
kcoff
plot (nbanew[c("FGP", "X3pp")], col=kcoff$cluster)
ww=cbind(NBA_2013_to_2014_SW, kcoff$cluster)
         ww=ww[order(ww[,ncol(ww)]),]
View(ww)         
write.table(ww, "offense.csv", sep=",")
nbadef=nbanew[c("A_DEF","SPG", "BPG", "MPG")]
kcdef = kmeans(nbadef, 5)
kcdef
plot (nbanew[c("BPG", "SPG")], col=kcdef$cluster)
ww=cbind(NBA_2013_to_2014_SW, kcdef$cluster)
ww=ww[order(ww[,ncol(ww)]),]
write.table(ww, "defense.csv", sep=",")
nbauti=nbanew[c("G", "FGP", "APG", "TO")]
kcuti = kmeans(nbauti, 5)
kcuti
plot (nbanew[c("G", "TO")], col=kcuti$cluster)
ww=cbind(NBA_2013_to_2014_SW, kcuti$cluster)
ww=ww[order(ww[,ncol(ww)]),]
write.table(ww, "utility.csv", sep=",")
