library("readr")
allLines <- function(){
	tes<-readr::read_file("Industrial_Society.txt")|>strsplit("\n\n")
	tes<- tes[[1]]
	a<-tibble::new_tibble(data.frame(x=matrix(unlist(tes), 
		nrow=299, byrow=TRUE),stringsAsFactors=FALSE)
)
	myFun<- function(ax) {
		if(nchar(ax)< 100 & 
		!any(startsWith(ax,strsplit(paste(unlist(1:9),collapse=' ')," ")[[1]])))
	{ return(TRUE)} else{return(FALSE)}}
	a$b<- sapply(a$x,myFun)
	return (a)
}
#r_num<-sample(1:length(tes[[1]]), 1)
#gsub("\n"," ",tes[[1]][[r_num]])|>print()
