gameStrategy <- function(GoalP){
  
  score = GoalP+1
  # Generate all combinations
  combinations = expand.grid(seq(score-2),seq(score-1))     
  combinations$sum = combinations[[1]] + combinations[[2]]
  # Finding max sum
  combinations = combinations[order(combinations$sum, decreasing = TRUE),]
  
  U = array(NA, dim=c(score+5,score+5,score+5))  
  V = array(NA, dim=c(score+5,score+5,score+5))  
  
  
  # boundary conditions
  # you win if you have 100+ points then 
  V[score:(score+5),,] = 1    
  # you win if it is your turn and you have goal-1 points
  V[score-1,seq(1,score-1),] = 1 
  # you loose if opponent reaches goal first
  V[seq(1,score-1),score:(score+5),] = 0    
  
  V[,seq(1,score-1),score:(score+5)] = 1   
  
  # Fill UV matrix based on game rules
  for(r in 1:length(combinations$sum)){
    for(t in (score):1){
      
      o = combinations[r,2]
      s = combinations[r,1]
      
      V[s,o,t] = max( ( (1/6)*(1-V[min(o,score),s+1,1]) + (1/6)*V[min(s,score),o,min(t+2,score)] + (1/6)*V[min(s,score),o,min(t+3,score)]
                        + (1/6)*V[min(s,score),o,min(t+4,score)] + (1/6)*V[min(s,score),o,min(t+5,score)]
                        + (1/6)*V[min(s,score),o,min(t+6,score)] ), 1-V[min(o,score),min(s+max(t-1,1),score),1] ) 
      
      U[s,o,t] = which.max( c( ( (1/6)*(1-V[min(o,score),s+1,1]) + (1/6)*V[min(s,score),o,min(t+2,score)] + (1/6)*V[min(s,score),o,min(t+3,score)]
                                 + (1/6)*V[min(s,score),o,min(t+4,score)] + (1/6)*V[min(s,score),o,min(t+5,score)]
                                 + (1/6)*V[min(s,score),o,min(t+6,score)] ), 1-V[min(o,score),min(s+max(1,t-1),score),1] ) ) 
    }
  }
  save(list = c('V','U'),file = 'VUfile.Rdata')
  
}


