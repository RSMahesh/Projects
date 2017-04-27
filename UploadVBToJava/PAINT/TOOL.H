/*FILE TOOL.H*/

#include"save.h"


int rubber() /*function for rubber */
{
		  int button,x,y,p[8],tc,px,py;
		  tc=getcolor();
		  getmouse(&button,&x,&y);
you:
		  if(kbhit())
		  check();

		  while(button==1)
		  {
			  getmouse(&button,&x,&y);
		  }

		  while(1)
		  {

			  if(kbhit())
		  check();


			  if(button!=1)
			  showmouse();
			  getmouse(&button,&x,&y);

			  if(button==1)
			  {
				  hidemouse();
				  if(!check())
				  {
					  setcolor(tc);
					  return;
				  }
				  if(check()==2)
					  goto you;

				  if(px!=x ||  py!=y)
				  {
					  x-=100;
					  y-=70;
					  p[0]=x,p[1]=y,p[2]=x,p[3]=y+10,p[4]=x+10,p[5]=y+10,p[6]=x+10,p[7]=y;
					  setfillstyle(1,WHITE);
					  fillpoly(4,p);
					  setcolor(WHITE);
					  rectangle(x,y,x+10,y+10);
					  x+=100;
					  y+=70;
					  setcolor(tc);
				  }
			  }

			  px=x;py=y;
		  }
}


spray()   /*function for spraying colors*/
{
	int button,x,y,tc;
	tc=getcolor();
	getmouse(&button,&x,&y);

you:
	while(button==1)
	{
		getmouse(&button,&x,&y);
	}

	while(1)
	{

		  if(kbhit())
		  check();

		if(button!=1)
			showmouse();
		getmouse(&button,&x,&y);

		if(button==1)
		{
			hidemouse();
			if(!check())
				return;
			if(check()==2)
				goto you;
			delay(10);
			putpixel(x-100+(random(100)%10),y-70+(random(100)%10),getcolor());
		}
		getmouse(&button,&x,&y);

	}

}


freehand()  /*function for free hand drawing*/
{
	int button,x,y,prex,prey;
	getmouse(&button,&x,&y);
	showmouse();

you:
	while(button==1)
	{
		getmouse(&button,&x,&y);
	}

	while(1)
	{
		if(kbhit())
		  check();

		getmouse(&button,&x,&y);
		if(button==1)
		{
			if(!check())
				return;
			if(check()==2)
				goto you;
			x-=100;
			y-=70;
			hidemouse();
			prex=x;
			prey=y;
			while(button==1)
			{
				line(prex,prey,x,y);
				prex=x;
				prey=y;
				getmouse(&button,&x,&y);
				x-=100;
				y-=70;
			}

			showmouse();

		}

	}

}



mline()/*function for making line*/
{
	int button,x,y,tog=0,fx,fy,px,py,tc,dx,dy,i,I;
	int x4,y4,n=0,a,b,a1,b1;
	unsigned area;
	char  *buff[4];
	getmouse(&button,&x,&y);

you:
	while(button==1)
	{
		getmouse(&button,&x,&y);
	}

	while(1)
	{
		if(kbhit())
		  check();

		if(button!=1)
			showmouse();
		if(button ==1)
		{
			if(!check())
				return;

			if(check()==2)
				goto you;
			x-=100;y-=60;
			clip(&x,&y,&fx,&fy);
			fx=x;fy=y;
			n=0;
			while(button==1)
			{
				if(px!=x || py!=y)
				{
					if(n==1)
					{
						dx=abs(fx-(px));
						dy=abs(fy-(py));
						a=0;b=0;a1=0;b1=0;
						if(px<fx)
						{
							dx=-dx;
							a=-5;
							a1=1;
						}
						if(py<fy)
						{
							dy=-dy;
							b=-5;
							b1=1;
						}
						x4=dx/4;
						y4=dy/4;

						putimage(fx+(x4*a1+a),fy+(y4*b1+b),buff[0],0);
						putimage(fx+x4+(x4*a1+a),fy+y4+(y4*b1+b),buff[1],0);
						putimage(fx+x4*2+(x4*a1+a),fy+y4*2+(y4*b1+b),buff[2],0);
						putimage(fx+x4*3+(x4*a1+a),fy+y4*3+(y4*b1+b),buff[3],0);

						for(i=0;i<4;i++)
						free(buff[i]);
					}
					a=5;b=5;
					if(x<5)
						x=5;
					dx=abs(fx-(x  ));
					dy=abs(fy-(y ));
					if(x <fx)
					{
						dx=-dx;
						a=-5;
					}
					if(y <fy)
					{
						dy=-dy;
						b=-5;
					}
					x4=dx/4;
					y4=dy/4;

					area=imagesize(fx,fy,fx+x4+a,fy+y4+b);
					for(i=0;i<4;i++)
					{
						buff[i]=malloc(area);
						if(buff[i]==NULL)
						  { for(I=0;I<i;I++)
						    free(buff[I]);

						    goto you;




						   }

					}
					hidemouse();
					getimage(fx,fy,fx+x4+a,fy+y4+b,buff[0]);
					getimage(fx+x4,fy+y4,(fx+x4*2)+a,(fy+y4*2)+b,buff[1]);
					getimage(fx+x4*2,fy+y4*2,(fx+x4*3)+a,(fy+y4*3)+b,buff[2]);
					getimage(fx+x4*3,fy+y4*3,(fx+x4*4)+a,(fy+y4*4)+b,buff[3]);
					n=1;

					line(fx,fy,x ,y  );
				}

				px=x;py=y;
				getmouse(&button,&x,&y);
				x-=100;y-=60;
				clip(&x,&y,&px,&py);

			}

		}
		 getmouse(&button,&x,&y);
	}




		}


		fill()/*function for filling colors */
		{
			int button,x,y,prex,prey,tc;
			getmouse(&button,&x,&y);

you:
			while(button==1)
			{
				getmouse(&button,&x,&y);
			}
			showmouse();

			while(1)
			{
				if(kbhit())
		  check();

				getmouse(&button,&x,&y);
				setfillstyle(1,bk);
				if(button==1)
				{
					if((x>=10 && x<=80) && (y>=100 && y<=234))
						return;
					if((x>=20 && x<=228) && (y>=450 && y<=462))
					{
						tc=getcolor();
						bk=msetcolor(x,y);
						setfillstyle(1,bk);
						setcolor(tc);
					}
					if((x>=601 && x<=614)&&(y>=21 && y<=39))
						exit(1);
					if((x>=180 &&x<=250)&& (y>=13 && y<=30))
					{
						SAVE();
						goto you;
					}
					if((x>=90 &&x<=160)&& (y>=13 && y<=30))
					{
						OPEN();
						goto you;
					}
					if((x>=10 &&x<=80)&& (y>=13 && y<=30))
					{
						NEW();
						goto you;
					}

				}
				showmouse();

				if(button==2)
				{
					hidemouse();
					x-=100;
					y-=65;
					front=0;
					bfill(x,y,bk,getpixel(x,y));
					showmouse();
				}

			}

		}



		mellip() /*function for making ellipse*/
		{
			int button,x,y,tog=0,fx,fy,px,py,tc,n;
			char  *z;
			getmouse(&button,&x,&y);
you:
			while(button==1)
			{
				getmouse(&button,&x,&y);
			}

			while(1)
			{

				if(kbhit())
		  check();

				if(button !=1)
					showmouse();
				if(button==1)
				{
					if(!check())
						return;
					if(check()==2)
						goto you;
					hidemouse();
					if(tog==0)
					{
						tog=1;
						fx=x-100;fy=y-50;
						n=0;
						if(start!=NULL)
							efree();
					}
					if(tog==1)
					{
						if(px!=x || py!=y)
						{
							tc=getcolor();
							if(n==1)
								redraw();
							setcolor(tc);
							elllip(x-100,y-50,abs(x-100-fx),abs(y-60-fy),tc);
							n=1;
						}
					}
				}

				px=x;py=y;
				getmouse(&button,&x,&y);
				if(!(button==1))
					tog=0;

			}

		}

		brush()  /*function for brush*/

		{
			int button,x,y,prex,prey;
			getmouse(&button,&x,&y);
if(kbhit())
		  check();

you:
			while(button==1)
			{
				getmouse(&button,&x,&y);
			}
			showmouse();

			while(!kbhit())
			{
				getmouse(&button,&x,&y);
				if(button==1)
				{
					if(!check())
						return;
					if(check()==2)
						goto you;
					hidemouse();
					x-=100;
					y-=70;
					prex=x;
					prey=y;
					while(button==1)
					{
						line(prex,prey,x,y);
						line(prex+1,prey,x+1,y);
						line(prex-1,prey,x-1,y);
						line(prex-2,prey,x-2,y);
						line(prex+2,prey,x+2,y);
						line(prex-3,prey,x-3,y);
						line(prex+3,prey,x+3,y);
						line(prex-4,prey,x-4,y);
						line(prex+4,prey,x+4,y);
						line(prex-5,prey,x-5,y);
						line(prex+5,prey,x+5,y);
						prex=x;
						prey=y;
						
						getmouse(&button,&x,&y);
						x-=100;
						y-=70;
						
					}

					showmouse();

				}
			}

		}

		rectk() /*function for rectangle*/
		{
			int sx,sy,tx,ty,x1=0,y1,x2,y2,button,x,y,tc,R;
			getmouse(&button,&x,&y);

you1:
			while(button==1)
			{
				getmouse(&button,&x,&y);
			}
you:
			tc=getcolor();

			while(1)
			{
				showmouse();
				getmouse(&button,&tx,&ty);
				if(kbhit())
				check();

				if(button==1)
				{
					if(!check())
					{
						free(m1);free(m2);free(m3);free(m4);
						return;
					}
					if(check()==2)
						goto you;
					tc=getcolor();
					hidemouse();
					sx=x=x1=x2=tx-100;
					sy=y=y1=y2=ty-50;
					setcolor(tc);
					clip(&x1,&y1,&x2,&y2);
					R=save1(x1,y1,x2,y2);
					if(!R)
						goto you;
					rectangle(x1,y1,x2,y2);
					getmouse(&button,&tx,&ty);
					tx-=100;
					ty-=50;

					while(button==1)
					{
						if(x!=tx || y!=ty)
						{
							tc=getcolor();
							restore1(x1,y1,x2,y2);
							x=tx;y=ty;

							if(x<sx)
							{x1=x;x2=sx;}

							else
							{x1=sx;x2=x;}

							if(y<sy)
							{ y1=y;y2=sy;}

							else
							{ y1=sy;y2=y;}

							setcolor(tc);
							clip(&x1,&y1,&x2,&y2);
							R=save1(x1,y1,x2,y2);
							if(!R) goto you;
							rectangle(x1,y1,x2,y2);
						}
						getmouse(&button,&tx,&ty);
						tx-=100;ty-=50;
					}

					tc=getcolor();

				}

			}
		}



	       elllip(int xc,int yc,int rx,int ry,int col)
		   {
			   float p,px,py,ry2,rx2,tworx2,twory2;
			   int x,y,xcenter,ycenter;
			   char *z;
			   xcenter=xc;ycenter=yc;
			   ry2=ry*ry;rx2=rx*rx;
			   twory2=2*ry2;tworx2=2*rx2;
			   x=0;y=ry;
			   plotpoints(xcenter,ycenter,x,y,col);
			   p=(ry2-rx2*ry+(0.25*rx));
			   px=0;py=tworx2*y;
			   while(px<py)
			   {
				   x=x+1;px=px+twory2;
				   if(p>=0)
				   {
					   y=y-1;py=py-tworx2;
				   }
				   if(p<0)
					   p=p+ry2+px;
				   else
					   p=p+ry2+px-py;
				   plotpoints(xcenter,ycenter,x,y,col);
			   }
			   
			   p=(ry2*(x+0.5)*(x+0.5)+rx2*(y-1)*(y-1)-rx2*ry2);
			   while(y>0)
			   {
				   y=y-1;py=py-tworx2;
				   if(p<=0)
				   {
					   x=x+1;px=px+twory2;
				   }
				   if(p>0)
					   p=p+rx2-py;
				   else
					   p=p+rx2-py+px;
				   plotpoints(xcenter,ycenter,x,y,col);
			   }
			   
		   }

		   /*function for ploting pixel of ellipse*/
		   plotpoints(int xcenter,int ycenter,int x,int y,int col)
		   {
			   
			   add(xcenter+x,ycenter+y);
			   add(xcenter-x,ycenter+y);
			   add(xcenter+x,ycenter-y);
			   add(xcenter-x,ycenter-y);
			   putpixel(xcenter+x,ycenter+y,col);
			   putpixel(xcenter-x,ycenter+y,col);
			   putpixel(xcenter+x,ycenter-y,col);
			   putpixel(xcenter-x,ycenter-y,col);
		   }
		   
		   
		   redraw()
		   {
			   struct spoint *q;
			   if(start==NULL)
				   return;
			   P=start;
			   while(P!=NULL)
			   {
				   putpixel(P->x,P->y,P->col);
				   q=P;P=P->next;free(q);
			   }
			   start=end=P=NULL;
		   }
		   
		   add(int x,int y)/*function for adding node in link list*/
		   {
			   
			   struct spoint *q;
			   static int d=0;d++;
			   k=malloc(sizeof(struct spoint));
			   if(k==NULL)
			   { exit(1); }
			   k->x=x; k->y=y;k->col=getpixel(x,y);
			   k->next=NULL;
			   if(start==NULL)
			   { start=end=k; }
			   else
			   {
				   end->next=k;
				   end=end->next;
			   }
		   }
		   
		   efree()/*function for freeing link list*/
		   {
			   struct spoint *q;
			   if(start==NULL)
				   return;
			   P=start;
			   while(P!=NULL)
			   { q=P;P=P->next;free(q); }
			   start=end=P=NULL;
		   }
		   
		   rot()/*function for 3D rotation*/
		   {
			   int ch=0,x,y,button;
			   static int lvalue=0;
			   getmouse(&button,&x,&y);
you1:
			   while(button==1)
			   {
				   getmouse(&button,&x,&y);
			   }
			   while(1)
			   {
				   ch=0;you:
			   getmouse(&button,&x,&y);
			   showmouse();
			   if(button==1)
			   {
				   if(!check())
					   return;
				   if(check==2)
					   goto you1;
				   if((x>=10 && x<=43) && (y>=279 && y<=279+40) )
					   lvalue= ch=2;
				   if((x>=44 && x<=79) && (y>=279 && y<=279+40) )
					   lvalue= ch=-2;
				   if((x>=10 && x<=43) && (y>=279+41 && y<=279+41+40))
					   lvalue= ch=3;
				   if((x>=44 && x<=79) && (y>=279+41 && y<=279+41+40))
					   lvalue= ch=-3;
				   if((x>=10 && x<=43) && (y>=279+41+41 && y<=279+41+41+40))
					   lvalue= ch=1;
				   if((x>=44 && x<=79) && (y>=279+41+41 && y<=279+41+41+40))
					   lvalue= ch=-1;
				   if(ch==0)goto you;
				   mul(p11,p10);mul(p22,p20);rot1(ch);
				   
			   }
			   }
			   
		   }
