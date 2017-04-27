
		      /* FILE SAVE.H */

#include"fcntl.h"
#include"types.h"
#include"stat.h"
#include"util.h"

ss(char *fname)	/*function for saving file*/
{
	int midx,midy,maxx,maxy,h;
	midx=(getmaxx()-(101+31))/2;
	midy=(getmaxy()-(60+60))/2;
	maxx= getmaxx()-(101+31);
	maxy=getmaxy()-(60+60);
	hidemouse();
	h=open(fname,O_CREAT|O_BINARY|O_WRONLY,S_IWRITE);
	if(h==-1)
	{
		msgbox("ERROR IN OPENING FILE");
		getch();
	       //	exit(1);
	       return;
	}
	// rectangle(midx+1,0,maxx,midy);f=fopen("mb.p","w");
				getimage(0,0,midx,midy,mp);
				write(h,mp,a);
				getimage(midx+1,0,maxx,midy,mp);
				write(h,mp,a);
				getimage(0,midy+1,midx,maxy,mp);
				write(h,mp,a);
				getimage(midx+1,midy+1,maxx,maxy,mp);
				write(h,mp,a);

				close(h);
				showmouse();

}


rr(char *fname)	/*function for reading file*/

{
	int midx,midy,maxx,maxy,h;
	midx=(getmaxx()-(101+31))/2;
	midy=(getmaxy()-(60+60))/2;
	maxx= getmaxx()-(101+31);
	maxy=getmaxy()-(60+60);
	hidemouse();
	h=open(fname,O_RDONLY|O_BINARY);
	if(h==-1)
	{
		msgbox("Erorr in opening File (rr)");
		getch();
		return;

			}

				read(h,mp,a);
				putimage(0,0,mp,0);
				read(h,mp,a);
				putimage(midx+1,0,mp,0);
				read(h,mp,a);
				putimage(0,midy,mp,0);
				read(h,mp,a);
				putimage(midx+1,midy+1,mp,0);
				close(h);
				showmouse();

}


/*function for save as and open box*/
box(char *bname)
{
				int tc;
				tc=getcolor();
				setcolor(8);
			       if(!mwrite())
			       {
			       msgbox("                          mahesh");
			       getch();
			       closegraph();
			       exit(1);
			       getch();
			       return 0;
			       }
			       rectangle(180,70,340,340);
				setfillstyle(1,8);
				floodfill(205,285,8);
				setfillstyle(1,11);
				setcolor(11);
				rectangle(170,60,330,330);
				floodfill(205,205,11);
				setcolor(8);
				rectangle(171,61,329,329);
				rectangle(173,63,327,327);
				settextstyle(1,0,1);
				setcolor(RED);
				setcolor(8);
				rectangle(190,100,310,120);
				setfillstyle(1,BLUE);
				floodfill(195,105,8);
				settextstyle(8,0,1);
				rectangle(190,150,310,290);
				setcolor(8);
				outtextxy(185,125,"File");
				outtextxy(185,75,"NAME");
				setfillstyle(1,GREEN);
				floodfill(195,155,8);
				pbut(190,300," OK ",1,0);
				pbut(255,300,"Cancel",1,0);
				pbut(220,60,bname,0,15);
				tc=men();
				fclose(f2);
				setcolor(tc);
				showmouse();
				return tc;
}


men() /*function for selecting file*/
{
	int pos=0,h=0,n=0,rec=0,curpos=0,tog=0,a,button,x,y,tc;
	char ch[2],name[11];
	settextstyle(2,0,6);
	ch[1]='\0';
	f2=fopen("painf2.xyz","r");
	if(f2==NULL)
	{
	 f2=fopen("paint2.xyz","w");
//return;

       }
       //	printf("mahesh bailwal");
	Nrecords=0;

	while(fread(name,10,1,f2)==1)
	Nrecords++;


	name[10]='\0';
	readf(0);

				while(1)
				{
					showmouse();
					while(!kbhit())
					{
						n=1;
						getmouse(&button,&x,&y);
						if(button==1 && (x>=355&& x<=395) && (y>=360 && y<=372))
						{
							hidemouse();
							goto you1;
						}
						if(button==1 && (x>=290&& x<=320) && (y>=360 && y<=372))
						{
							hidemouse();
							goto you;
						}

					}


					if(kbhit())
					{
						n=0;
						getkey();

					}
					if(ascii!=0)
					{
						hidemouse();
						if(ascii==27)
						{
you1:
						mread();
						return 0;
						}

						if(ascii==8)
						{
							setfillstyle(1,BLUE);
							bar(190,100,310,120);
							setfillstyle(1,WHITE);
							pos=0;
							continue;
						}

						if(ascii=='\r')
						{
you:
						fname[pos]='\0';
						if(tog==1)
						{
							fseek(f2,10*(rec),SEEK_SET);
							fread(name,10,1,f2);
							for(a=0;a<10;a++)
							{
								if(name[a]=='0')
								{
									fname[a]='\0';
									break;
								}
								fname[a]=name[a];
							}

						}

						mread();
						return 1;

						}
						ch[0]=ascii;
						ch[0]= toupper(ch[0]);
						if(pos<=10)
						{
							hidemouse();
							tc=getcolor();
							setcolor(WHITE);
							fname[pos]=ch[0];
							outtextxy(195+pos*10,100,ch);
							setcolor(tc);
							showmouse();
						}
						pos++;



					}

					else
					{
						if(scan!=0)
						{
							hidemouse();

							if(scan==80)
							{

								if(tog==0)
								{

								       fseek(f2,10*(rec),SEEK_SET);
								       name[0]='^';
								       fread(name,10,1,f2);
								       if(name[0]=='^'||name[0]==EOF)
								       continue;


									setfillstyle(1,WHITE);
									bar(190,150+(curpos)*20,310,170+(curpos)*20);
									outtextxy(195,148+(curpos)*20,name);
									tog=1;
									continue;
								}




								if(curpos>=6)
								{
									rec++;

									if(readf(rec-curpos)==0)
									rec--;
									setfillstyle(1,WHITE);

									bar(190,150+(curpos)*20,310,170+(curpos)*20);

									fseek(f2,10*(rec),SEEK_SET);
									fread(name,10,1,f2);
									outtextxy(195,148+(curpos)*20,name);

									continue;
								}

								if(rec+1>=Nrecords)
								continue;


								setfillstyle(1,GREEN);
								fseek(f2,10*(rec),SEEK_SET);
								bar(190,150+(curpos)*20,310,170+(curpos)*20);
								fread(name,10,1,f2);

								outtextxy(195,148+(curpos)*20,name);

								setfillstyle(1,WHITE);
								bar(190,150+(curpos+1)*20,310,170+(curpos+1)*20);

								fseek(f2,10*(rec+1),SEEK_SET);
								fread(name,10,1,f2);
								outtextxy(195,148+(curpos+1)*20,name);

								if(curpos!=6)
								curpos++;
								rec++;



							}

							if(scan==72)
							{

								if(curpos<=0)
								{
								if(Nrecords<6)
									 continue ;
									rec--;
									if(readf(rec-curpos)==0)
									rec=0;
									setfillstyle(1,WHITE);

									bar(190,150+(curpos)*20,310,170+(curpos)*20);
									fseek(f2,10*(rec),SEEK_SET);
									fread(name,10,1,f2);
									outtextxy(195,148+(curpos)*20,name);


								       continue;
								}
								if(curpos>0)
								{
									setfillstyle(1,GREEN);
									fseek(f2,10*(rec),SEEK_SET);
									bar(190,150+(curpos)*20,310,170+(curpos)*20);
									fread(name,10,1,f2);
									outtextxy(195,148+(curpos)*20,name);
								}
								setfillstyle(1,WHITE);
								bar(190,150+(curpos-1)*20,310,170+(curpos-1)*20);
								fseek(f2,10*(rec-1),SEEK_SET);
								fread(name,10,1,f2);
								outtextxy(195,148+(curpos-1)*20,name);

								if(curpos!=0)
								{
									curpos--;
									rec--;
								}
							}


						}
    }
}

}
/* function for showing bitmap files name in box*/
readf(int mov)
{
	int h=0,no=0,num=0,temp;
	char name[11];
	name[10]='\0';
	setcolor(RED);

	fseek(f2,0,SEEK_SET);

	while(fread(name,10,1,f2)==1)
	num++;



	fseek(f2,10*mov,SEEK_SET);

	while(fread(name,10,1,f2)==1)
	no++;
	if(no<6 && num>6)
	return 0;
	setfillstyle(1,GREEN);
	bar(190,150,310,290);
    //   if(num<6)
   //  fseek(f2,0,SEEK_SET);
  //	else
	fseek(f2,10*mov,SEEK_SET);
       //temp= getcolor();
       //	setcolor(BLUE);

	while(fread(name,10,1,f2)==1)
	{
		if(h>6 || num<=h)
		break;

		outtextxy(195,148+h*20,name);
		h++;
	}

       //	setcolor(temp);

	return 1;
}


/*function for showing selected tool*/
select(int x1,int y1,int x2,int y2)
{
	int dc;
	dc=getcolor();
	setcolor(4);
	setviewport(0,0,getmaxx(),getmaxy(),0);
	hidemouse();
	rectangle(x1,y1,x2,y2);
			 showmouse();
			 setcolor(dc);
			 setviewport(101, 60, getmaxx()-31, getmaxy()-60, 1);

}

/*function for showing deselected tool*/

deselect(int x1,int y1,int x2,int y2)
{
	int dc;
	dc=getcolor();
	setcolor(7);
	setviewport(0,0,getmaxx(),getmaxy(),0);
	hidemouse();
	rectangle(x1,y1,x2,y2);
	showmouse();
	setviewport(101, 60, getmaxx()-31, getmaxy()-60, 1);
	setcolor(dc);

}



/*function for creating box  containing string*/
pbut(int x, int y,char *ch,int s,int col)
{
	int w,fc=3,i=0,te;
	char sh[2];
	sh[1]='\0';
	if(col!=0)
	fc=col;
	settextstyle(2,0,5);
	w=textwidth(ch);
	if(s!=0)
	{
		setcolor(8);
		setfillstyle(1,8);
		rectangle(x+4,y+3,x+4+w+2,y+22);
		bar(x+4+1,y+3+1,x+4+w+2-1,y+22-1);

	}

	setfillstyle(1,fc);
	setcolor(RED);
	rectangle(x+0,y+0,x+w+2,y+17);
	bar(x+0+1,y+0+1,x+w+2-1,y+17-1);



	outtextxy(x+2,y+1,ch);

	/* i=0;
	 te=strlen(ch);
	 while(i<te)
	 {
	   sh[0]=(i+48);
	   outtextxy(x+2,y+1,sh);
	   x=+5;
	   printf("%d",i);
	   i++;

	  }
	  getch();
	  //  sh[0]=(i+48);
	 // outtextxy(x+20,y+1,"KLHGGJGHJHJ");
	 */

}

/*function for saving screen covered by open or save as box*/
mwrite()
{
	int h;
	char *buf;
	hidemouse();
	marea=imagesize(170,60,340,200);
	buf=malloc(marea);
	if(buf==NULL)
	{
		msgbox("No memory mwrite12");
		getch();
		closegraph()

		exit(1);



	}
	getimage(170,60,340,200,buf);
	h=open("image.bail",O_CREAT|O_BINARY|O_WRONLY,S_IWRITE);
	if(h==-1)
	{
		msgbox("Not able to open file ");
		getch();
		closegraph();

		exit(1);
	}
	write(h,buf,marea);
	getimage(170,201,340,340,buf);
	write(h,buf,marea);
	free(buf);
	close(h);
	showmouse();
}

/*function for restoring screen covered by open or save as box*/
mread()
{

	int h;
	char *buf;
	hidemouse();
	buf=malloc(marea);
	if(buf==NULL)
	{
	      msgbox("NONONONONONPNO");
		getch();
		exit(1);
	}

	h=open("image.bail",O_RDONLY|O_BINARY);

		  if(h==-1)
		  {
			  msgbox("NOt open");
			  getch();
			  exit(1);
		  }

		  read(h,buf,marea);

		  putimage(170,60,buf,0);

		  read(h,buf,marea);

		  putimage(170,201,buf,0);
		  free(buf);
		  close(h);
		  showmouse();

}


SAVE()
{
	FILE *f;
	int tog=0,a,tc;
	tc=getcolor();
	if(!box("Save as"))
	{
		setcolor(tc);
		return;
	}
	ss(fname);
	for(a=0;a<10;a++)
	{
		if(fname[a]=='\0')
			tog=1;
		if(tog==1)
			fname[a]='0';
	}


	f=fopen("painf2.xyz","a");
	if(f==NULL)
	{
		msgbox("NO,NO,no.");
		getch();
		exit(1);
	}
	fwrite(fname,10,1,f);
	wfilename(fname);
	setcolor(tc);
	fclose(f);
}


OPEN()
{
	int tc;

	tc=getcolor();
	if(!box("Open file"))
	{
		setcolor(tc);
		return;
	}
	rr(fname);
	wfilename(fname);
	setcolor(tc);
}

NEW()
{
	setfillstyle(1,15);
	bar( 0, 0, getmaxx()-30, getmaxy()-59);
	wfilename("Untitled");
}
wfilename(char *f)
{
	int tc;
	tc=getcolor();
	setviewport(0,0,getmaxx(),getmaxy(),1);
	setfillstyle(1,WHITE);
	bar(300,40,400,60);
	setcolor(BLUE);
	line(300,59,400,59);
	line(300,55,400,55);
	setcolor(tc);
	pbut(310,42,f,0,11);
	setviewport(101,60,getmaxx()-31,getmaxy()-60,1);
}



/*utility function of rectk function*/
save1(int x1,int y1,int x2,int y2)
{
	   unsigned area1,area2,area3,area4,a;

	   area1=imagesize(x1,y1,x1,y2);
	   m1=malloc(area1);
	   area2=imagesize(x1,y1,x2,y1);
	   m2=malloc(area2);
	   area3=imagesize(x2, y1,x2,y2);
	   m3=malloc(area3);
	   area4=imagesize(x1,y2,x2,y2);
	   m4=malloc(area4);

	   if(m1==NULL || m2==NULL ||m3==NULL || m4==NULL)
	   {
		   free(m1);
		   free(m2);
		   free(m3);
		   free(m4);
		   return 0;
	   }

	      getimage(x1,y1,x1,y2,m1);
		  getimage(x1,y1,x2,y1,m2);
		  getimage(x2,y1,x2,y2,m3);
		  getimage(x1,y2,x2,y2,m4);
		  return 1;

}


/*utility function of rectk function*/

restore1(int x1,int y1,int x2,int y2)
{
	putimage(x1,y1,m1,0);
	putimage(x1,y1,m2,0);
	putimage(x2,y1,m3,0);
	putimage(x1,y2,m4,0);


	free(m1);
	free(m2);
	free(m3);
	free(m4);

}



	  save(int x1,int y1,int x2,int y2)
	   {
	   unsigned area1,area2,area3,area4,a;
	  // shuffle(&x1,&y1,&x2,&y2);
	   midx=x1+(x2-x1)/2;
	   midy=y1+(y2-y1)/2;

	   area1=imagesize(x1,y1,midx,midy);
	   M1=malloc(area1);
	   area2=imagesize(midx+1,y1,x2,midy);
	   M2=malloc(area2);
	   area3=imagesize(x1,midy+1,midx,y2);
	   M3=malloc(area3);
	   area4=imagesize(midx+1,midy+1,x2,y2);
	   M4=malloc(area4);

	   if(M1==NULL || M2==NULL ||M3==NULL || M4==NULL)
	   {
	  // a= imagesize(101,60,getmaxx()-31,getmaxy()-60);





	       free(M1);
	       free(M2);
	       free(M3);
	      free(M4);

	       exit(1);
	      }

	      getimage(x1,y1,midx,midy,M1);
	      getimage(midx+1,y1,x2,midy,M2);
	      getimage(x1,midy+1,midx,y2,M3);
	      getimage(midx+1,midy+1,x2,y2,M4);
	      return 1;

	      }

	      restore(int x1,int y1)
	      {
		putimage(x1,y1,M1,0);
		putimage(midx+1,y1,M2,0);
		putimage(x1,midy+1,M3,0);

		putimage(midx+1,midy+1,M4,0);


	       free(M1);
	       free(M2);
	       free(M3);
	      free(M4);

	       }

						//heap


msgbox(char *ms)
{
//int l;
//l=strlen(ms);
//rectangle(150,200,150+l*10,270);
//outtextxy(160,210,ms);

	pbut(150,200,ms,1,7);



}



