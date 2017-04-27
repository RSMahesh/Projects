




		   /*FILE NAME PAINT.C*/

		#include"tool.h"

		main(int argc ,char argv[]) /*main function of the program*/
		{

		int button,x,y,prex,prey,area;
		mp=malloc(a);
	       init("");
		//initscreen();

		clearviewport();
		setviewport(0, 0, getmaxx(), getmaxy(), 0);
	       if(!initmouse())
		{
	       printf("NO MOUSE SUPPORT");
	       getch();
		exit(1);
		}

		setfillstyle(1,15);
		setbkcolor(7);
		floodfill(10,10,WHITE);
		colorbar();         /*making color  bar for choosing color*/
		rotbar(25,292);
		setcolor(RED);
		settextstyle(2,0,5);



		pbut(180,16,"Save file",1,0);
		pbut(90,16,"Open file",1,0);
		pbut(10,16,"New file",1,0);
		outtextxy(14,83,"TOOLBAR");
		rectangle(48,432,115,447);
		outtextxy(50,432,"COLORBAR");
		rectangle(558,5,625,19);

		outtextxy(560,5,"CLOSEBOX");
		rectangle(10,255,77,269);
		outtextxy(12,255,"ROTATION");
		toolbar(-10,0);   /*creating tool bar*/



		setfillstyle(1,4);
		setcolor(GREEN);
		rectangle(290,445,310,465);
		closebox();
		setcolor(9);
		rectangle( 5,5, getmaxx()-5, getmaxy()-5);
		rectangle( 1, 1, getmaxx()-1, getmaxy()-1);
		rectangle( 100, 59, getmaxx()-30, getmaxy()-59);
		rectangle( 95, 55, getmaxx()-25, getmaxy()-55);



		setcolor(6);
		settextstyle(1,0,4);

		outtextxy(290,5,"PAINT");
		setviewport(101, 60, getmaxx()-31, getmaxy()-60, 1);
		showmouse();
		settextstyle(2,0,4);
	     //	msgbox("mahesh");


	   while(1)     /* start of main whileloop */
	   {

		   getmouse(&button,&x,&y);
		   showmouse();

		      if(kbhit())
			   {

			   getkey();
			//   if(scan==1)
			 //  printf("jk");
			   check();
			   scan=0;


			   }


		   if((button && 1)==1) /*start of if condition*/
		   {
						   check();

			   if((x>=10 && x<=43)&&(y>=102 && y<=126))
			   {
				   select(13,103,40,124);
				   freehand();
				   deselect(13,103,40,124);
			   }

			   if((x>=44 && x<=79)&&(y>=102 && y<=126))
			   {
				   select(46,103,77,125);
				   rectk();
				   deselect(46,103,77,125);
			   }

			   if((x>=10 && x<=43)&&(y>=127 && y<=154))
			   {
				   select(12,130,41,152);
				   mline();
				   deselect(12,130,41,152);
			   }


			   if((x>=44 && x<=79)&& (y>=127 && y<=154))
			   {
				   select(47,130,77,153);
				   spray();
				   deselect(47,130,77,153);
			   }

			   if((x>=10 && x<=43)&& (y>=155 && y<=184))
			   {
				   select(12,156,41,183);
				   rubber();
				   deselect(12,156,41,183);
			   }

			   if((x>=44 && x<=79)&& (y>=155 && y<=184))
			   {
				   select(47,156,77,183);
				   fill();
				   deselect(47,156,77,183);
			   }


			   if((x>=10 && x<=43)&& (y>=184 && y<=214))
			   {
				   select(12,186,41,208);
				   brush();
				   deselect(12,186,41,208);
			   }

			   if((x>=44 && x<=79)&& (y>=184 && y<=214))
			   {
				   select(47,186,77,208);
				   mellip();
				   deselect(47,186,77,208);
			   }

			   if((x>=10 &&  x<=79) && ( y>=184+27 &&  y<=184+49))
			   {
				   select(12,184+28,77,184+48);
				   rot();
				   deselect(12,184+28,77,184+48);

			   }


			   if((x>=10 &&  x<=79) && ( y>=184+50 &&  y<=184+70))
			   {
				   select(12,184+50,77,184+70);
				   bspline();
				   deselect(12,184+50,77,184+70);

			   }


			   if((x>=20 && x<=228) && (y>=450 && y<=462))
			   {
				   setviewport(0,0,getmaxx(),getmaxy(),0);
				   msetcolor(x,y);
				   setviewport(101, 60, getmaxx()-31, getmaxy()-60, 1);
			   }

		   } /*end of if condition*/



	   }  /*end of while loop*/

	   getch();
	   closegraph();
	} /* end of main function */


	

	
	
	
	
	
