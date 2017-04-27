	      /* File UTIL.H */
	    #include<graphics.h>
	    #include<dos.h>
	    #include<stdlib.h>
	    #include"math.h"
	    #include<stdio.h>
	   int cursor[32]={0X0000,0x0000,0x0000,0x0000,
				0x8001,0xc003,0xf00f,0xfc3f};

	       struct SREGS s;
	       /*Declaration of global variables*/
		struct spoint
		{
		int x,y,col;
		struct spoint *next;
		};

		struct point3
		{
		double x, y, z;
		};

			struct spoint  *start,*end,*P,*k;
			union REGS i,o;
		FILE *f2;
			char *buf,*m1,*m2,*m3,*m4 ,*mp,fname[11];
			char far *M1,*M2,*M3,*M4;

			int front,dc,bk,midx,midy,ascii,scan,Nrecords;
;
			unsigned  a=23046,marea;

		struct point3 p1[4]={-10,0*2,0,15*2,0*2,0*2,15*2,20*2,0,-10,20*2,0};
		struct point3 p2[4]={-10,0,15*2,15*2,0,15*2,15*2,20*2,15*2,-10,20*2,15*2};
		struct point3 p10[4]={-10,0*2,0,15*2,0*2,0*2,15*2,20*2,0,-10,20*2,0};
		struct point3 p20[4]= {-10,0,15*2,15*2,0,15*2,15*2,20*2,15*2,-10,20*2,15*2};
		struct point3 p11[4],p22[5];



			screen(char a[] )/*function for initalizing Graphic Screen*/
			{
				int gd=DETECT,gm,area;
				initgraph(&gd, &gm, " ");
			}

			hidemouse() /*function for hiding mouse cursur*/
			{
				i.x.ax=2;
				int86(0X33,&i,&o);

			}

			initmouse()/*function for initializing mouse */

			{
				i.x.ax=0;
				int86(0x33,&i,&o);
				return(o.x.ax);
			}


			getkey()/*function for getting key hitted*/
			{
				i.h.ah=0;
				int86(0x16,&i,&o);
				ascii=o.h.al;
				scan=o.h.ah;
			}


			showmouse()/*function for showing mouse cursur*/

			{
				i.x.ax=1;
				int86(0x33,&i,&o);
			}

			/*function for getting mouse status*/

			getmouse(int *button,int *x,int *y)
			{
				i.x.ax=3;
				int86(0x33,&i,&o);
				*button=o.x.bx;
				*x=o.x.cx;
				*y=o.x.dx;
			}

			/*function making shaping of spray tool*/

			sspray(int x,int y)
			{
				line(x,y+2,x+5,y-3);
				line(x+5,y-3,x+10,y+2);
				rectangle(x,y+2,x+10,y+10);
				line(x,y+10,x,y);
				line(x-7,y-8,x-7,y+1);
				line(x-7,y+1,x+5,y-5);																																																 line(x-7,y-8,x+5,y-5);
				setfillstyle(1,GREEN);
				floodfill(x-2,y-2,BLUE);
				setfillstyle(1,RED);
				floodfill(x+5,y+5,BLUE);

			}

			/*function making shaping of fill tool*/

			sfill(int x,int y)
			{
				line(x+5,y-2,x,y+5);
				line(x+15,y,x+10,y+7);
				line(x+5,y-2,x+15,y);
				line(x,y+5,x+10,y+7);
				setfillstyle(1,7);
				floodfill(x+12,y,BLUE);
				setcolor(GREEN);
				arc(x+13, y, 20, 135, 5);
				arc(x+13, y+1, 20, 135, 5);
			}

			/*function making  toolbar*/

			toolbar(int x,int y)
			{
				setcolor(BLUE);
				rectangle(x+20,y+100,x+90,y+184+70);
				setfillstyle(1,7);
				floodfill(x+50,y+110,BLUE);
				line(x+55,y+100,x+55,y+184+26);
				line(x+20,y+128,x+90,y+128);
				line(x+30,y+107,x+44,y+118);
				rectangle(x+68,y+107,x+78,y+115);
				line(x+20,y+154,x+90,y+154);
				line(x+30,y+140,x+50,y+140);
				sspray(x+72,y+140);
				line(x+20,y+184,x+90,y+184);
				rectangle(x+25,y+160,x+45,y+170);
				line(x+20,y+184+26,x+90,y+184+26);
				sfill(x+61,y+167);
				setcolor(BLUE);
				ellipse(x+70,y+195,0,360,10,5);
				sbrush(x+30,y+193);
				setcolor(BLUE);
				settextstyle(1,0,2);
				outtextxy(x+38,y+210,"3D");
				line(x+20,y+184+50,x+90,y+184+50);
				outtextxy(x+30,y+230,"B S");


			}

			closebox() /*function for making closebox*/
			{
				setcolor(RED);
				rectangle(600,22,615,42);
				setfillstyle(1,7);
				floodfill(605,30,RED);
				line(602,25,612,39);
				line(612,25,602,39);
				setcolor(GREEN);
			}

			 /*function for seting drawing color*/
			msetcolor(int x1,int y1)
		{
				int x=10,y=450,a,tc;
				for(a=0;a<16;a++)
				{
				if((x1>=x && x1<=x+13) && (y1>=y && y1<=y+12))
				{
				setcolor(a);
				setfillstyle(1,a);
				setviewport(0,0,getmaxx(),getmaxy(),0);
				if(a!=GREEN)
				floodfill(300,450,GREEN);
				setviewport(101, 60, getmaxx()-31, getmaxy()-60, 1);
				return a;
				}
				x=x+13;
				}
			}



			colorbar() /*function for showing color bar*/
			{
				int y=450,x=10,a;
				a=getcolor();
				setcolor(BLUE);
				setfillstyle(1,RED);
				rectangle(x-1,y-1,x+13*16+1,y+13);
				floodfill(x+2,y+2,BLUE);
				setcolor(15);
				for(a=0;a<16;a++)
				{
					setfillstyle(1,a);
					rectangle(x,y,x+13,y+12);
					floodfill(x+2,y+2,15);
					x=x+13;
				}
			}


			sbrush(int x,int y)/*this function contain shape of brush*/
			{
				int a;
				rectangle(x,y,x+12,y+6);
				setfillstyle(1,YELLOW);
				floodfill(x+4,y+4,BLUE);
				rectangle(x+5,y,x+8,y-7);
				setfillstyle(1,BLACK);
				floodfill(x+6,y-2,BLUE);
				x++;
				setcolor(15);
				for(a=0;a<5;a++)
				{
					line(x,y+7,x+1,y+14);
					line(x+1,y+14,x+2,y+7);
					x=x+2;
				}

			}

			rotbar(int x,int y)/*function for making rotation bar*/
			{
				setcolor(BLUE);
				rectangle(x-15,y-20,x+55,y+97);
				setfillstyle(1,7);
				floodfill(x,y,BLUE);
				line(x-15,y+20,x+55,y+20);
				line(x-15,y+60,x+55,y+60);
				line(x+18,y-20,x+18,y+97);
				setcolor(BLUE);
				ellipse(x, y, 30, 350, 5,10);
				setfillstyle(1,3);
				ellipse(x, y, 30, 350, 6,10);
				line(x+5,y,x,y+5);
				line(x+5,y,x+10,y+5);
				line(x+5,y-1,x,y+4);
				line(x+5,y-1,x+10,y+4);
				x+=35;
				ellipse(x, y, 12, 325, 5,10);
				ellipse(x, y, 12, 325, 6,10);
				line(x+5,y,x,y-5);
				line(x+5,y,x+10,y-5);
				line(x+5,y+1,x,y-4);
				line(x+5,y+1,x+10,y-4);
				x-=31,y+=40;
				line(2+x-10,y-7,2+x-16,y);
				line(2+x-10,y-7,2+x-2,y);
				line(2+x- 10,y-8,2+x-17,y);
				line(2+x-10,y-8,2+x-3,y);
				arc(x, y, 152, 350, 7);
				arc(x, y, 152, 350, 8);
				x+=32;
				arc(x, y, 180, 390, 7);
				arc(x, y, 152, 350, 8);
				x+=14;y+=+1;
				line(2+x-10,y-7,2+x-16,y);
				line(2+x-10,y-7,2+x-2,y);
				line(2+x- 10,y-8,2+x-17,y);
				line(2+x-10,y-8,2+x-3,y);
				x-=50,y+=40;
				ellipse(x, y, 70, 330, 10,5);
				ellipse(x, y, 70, 330, 11,5);
				line(x+9,y+3,x,y);
				line(x+9,y+3,x+4,y+10);
				line(x+10,y+3,x+1,y);
				line(x+10,y+3,x+5,y+10);
				x+=38;
				ellipse(x, y, 200, 80, 10,5);
				ellipse(x, y, 200, 80, 11,5);
				x-=2;y-=1;
				line(x-10,y+3,x,y);
				line(x-10,y+3,x,y+10);
				line(x-11,y+3,x-1,y);
				line(x-11,y+3,x-1,y+10);
			}

			cube()/*function for making cube*/
			{
				int i,R,hx,hy,lx,ly;
				static int n=0,px,py;
				change();
				if(n==1)
				restore(px,py);
				lx=hx=p1[0].x;ly=hy=p1[0].y;
				for(i=0;i<4;i++)
				{
					if(hx<p1[i].x)
					hx=p1[i].x;
					if(hx<p2[i].x)
					hx=p2[i].x;
					if(lx>p1[i].x)
					lx=p1[i].x;
					if(lx>p2[i].x)
					lx=p2[i].x;
					if(hy<p1[i].y)
					hy=p1[i].y;
					if(hy<p2[i].y)
					hy=p2[i].y;
					if(ly>p1[i].y)
					ly=p1[i].y;
					if(ly>p2[i].y)
					ly=p2[i].y;
				}
			R=save(lx-1,ly-1,hx+1,hy+1);
			if(!R)exit(1);
			px=lx-1;py=ly-1;
			n=1;you:
			for(i=0;i<4;i++)
			line(p1[i].x,p1[i].y,p2[i].x,p2[i].y);
		for(i=0;i<3;i++)
		{
			line(p1[i].x,p1[i].y,p1[i+1].x,p1[i+1].y);
			line(p2[i].x,p2[i].y,p2[i+1].x,p2[i+1].y);
		}
		line(p1[3].x,p1[3].y,p1[0].x,p1[0].y);

		line(p2[3].x,p2[3].y,p2[0].x,p2[0].y);

		}
		     /*function changing cordinates from 3D to 2D*/
			change()
			{
				int i,xmax,ymax;
				xmax = getmaxx();
				ymax = getmaxy();
				for(i=0;i<4;i++)
				{
				  p1[i].x*=2;
				  p1[i].y*=2;
				  p1[i].x+=p1[i].z;
				  p1[i].y+=p1[i].z;
				  p1[i].x=(xmax/2)+(p1[i].x);
				  p1[i].y=(ymax/2)-(p1[i].y);
				  p2[i].x*=2;p2[i].y*=2;
				  p2[i].x+=p2[i].z;p2[i].y+=p2[i].z;
				  p2[i].x=(xmax/2)+(p2[i].x);
				  p2[i].y=(ymax/2)-(p2[i].y);
				}

			}

			rot1(int D)/*function for rotation */
			{
			  double M[3][3]={0,0,0,0,0,0,0,0,0};
			  double T=0;
			  static float x=0,y=0,z=0;
			  int row,ele,n=0;
			  mul(p1,p10);
			  mul(p2,p20);

			if(D==-3||D==3)
			{
			   z=0.02;
			   if(D==-3)
			   z*=-1;
			   M[1][1]=1;
			   M[0][0]=cos(z);
			   M[0][2]=-sin(z);
			   M[2][0]=sin(z);
			   M[2][2]= cos(z);
			}
			else if(D==2||D==-2)
			{
			   y=0.02;
			   if(D==-2)
			   y=-y;
			   M[0][0]=1;
			   M[1][1]=cos(y);
			   M[1][2]=-sin(y);
			   M[2][1]=sin(y);
			   M[2][2]=cos(y);
			}

			else if(D==1 || D==-1)
			{
			   x=0.02;
			   if(D==-1)
			   x=-x;
			   M[2][2]=1;
			   M[0][0]=cos(x);
			   M[0][1]=sin(x);
			   M[1][0]=-sin(x);
			   M[1][1]= cos(x);
			}

		for(row=0;row<4;row++)
		{
		   for(ele=0;ele<3;ele++)
		     {
			T+=p1[row].x*M[0][ele];
			T+=p1[row].y*M[1][ele];
			T+=p1[row].z*M[2][ele];
			if(ele==0)
			p10[row].x=T;
			if(ele==1)
			p10[row].y=T;
			if(ele==2)
			p10[row].z=T;
			T=0;
		     }

	       }


	     for(row=0;row<4;row++)
	     {
		for(ele=0;ele<3;ele++)
		  {
		     T+=p2[row].x*M[0][ele];
		     T+=p2[row].y*M[1][ele];
		     T+=p2[row].z*M[2][ele];
		     if(ele==0)
		     p20[row].x=T;
		     if(ele==1)
		     p20[row].y=T;
		     if(ele==2)
		     p20[row].z=T;
		     T=0;
		}

	     }

			mul(p1,p10);
			mul(p2,p20);
			cube(3);
			}


			intl()/*function for refreshing cube dimension*/
			{
			   int a;
			   for(a=0;a<4;a++)
			   {
			      p1[a].x=p11[a].x;
			      p1[a].y=p11[a].y;
			      p1[a].z=p11[a].z;
			      p2[a].x=p22[a].x;
			      p2[a].y=p22[a].y;
			      p2[a].z=p22[a].z;
			   }

		     }
			/*utility function of rotation*/
			mul(struct point3  t[4],struct point3 t1[4])
			{
					int a;
				for(a=0;a<4;a++)
				{
				   t[a].x=t1[a].x;
				   t[a].y=t1[a].y;
				   t[a].z=t1[a].z;
				}

			}
		  /*function 1st for filling color*/
		  bfill(int x,int y,int fcol,int ocol)
		  {
			if(getpixel(x,y)!=ocol || getpixel(x,y)==fcol )
			return;
			putpixel(x,y,fcol);
			if(!front)
			bfill(x-1,y,fcol,ocol);
			else
			bfill(x+1,y,fcol,ocol);
			bfill(x,y-1,fcol,ocol);
			bfill(x,y+1,fcol,ocol);
			front=1;
		  }

		     /*function second for filling color*/
			bfill2(int x,int y,int fcol,int ocol)
			{
				if(getpixel(x,y)!=ocol)
				return;
				putpixel(x,y,fcol);
				bfill2(x+1,y,fcol,ocol);
				bfill2(x,y-1,fcol,ocol);
				bfill2(x,y+1,fcol,ocol);
			}


			initscreen()/*function for 1st screen of program*/
			{
			   screen("");
			   setcolor(8);
			   settextstyle(5,0,4);
			   outtextxy(170,380,"Press any key to continue");
			   setbkcolor(WHITE);
			   setviewport(70,30,getmaxx(),getmaxy(),0);
			   rr("mb.bit");
			   getch();

			}

			/*function for cliping */
			clip(int *x1,int *y1,int *x2,int *y2)
			{
				if(*x1<1)
				*x1=1;
				if(*x1>getmaxx()-131)
				*x1=getmaxx()-131;
				if(*x2<5)
				*x2=5;
				if(*x2>getmaxx()-131)
				*x2=getmaxx()-131;
				if(*y1<5)
				*y1=5;
				if(*y1>getmaxx()-121)
				*y1=getmaxx()-121;
				if(*y2<3)
				*y2=3;
				if(*y2>getmaxx()-121)
				*y2=getmaxx()-121;
			}

			/* utility function */
			menuch()
			{
			  int button,x,y;
			  getmouse(&button,&x,&y);
			  if((x>=10 && x<=80) && (y>=100 && y<=264))
			  return 0;
			  if((x>=20 && x<=228) && (y>=450 && y<=462))
			  msetcolor(x,y);
			  if( ((x>=601 && x<=614)&&(y>=21 && y<=39)) || scan==1)
			  {
			  closegraph();

			  exit(1);
			  }
			  if( ((x>=180 &&x<=250)&& (y>=13 && y<=30)) || scan==31 )
			  {
				SAVE();
				scan=0;
				return 2;
			  }

			  if( ((x>=90 &&x<=160)&& (y>=13 && y<=30)) ||scan==25)
			  {
				OPEN();
				scan=0;
				return 2;
			  }
			  if(  ((x>=10 &&x<=80)&& (y>=13 && y<=30)) || scan==49)

			  {
				NEW();
				scan=0;
				return 2;
			  }
				return 1;

			  }

			      /*utility function*/
				check()
				{
					int button,x,y;
					getmouse(&button,&x,&y);
					if(kbhit())
					getkey();

					//if(button==1 || kbhit())

					{
					if( (x<101|| y<60 ||y>getmaxy()-60) ||scan>0)
					return(menuch());
					}
				}



			    changez()
			    {
			    i.x.ax=9;
			    i.x.ax=0;
			    i.x.cx=0;
			    i.x.dx=(unsigned)cursor;
			    segread(&s);
			    s.es=s.ds;
			    int86x(0x33,&i,&i,&s);
			    }





			    int U[100]={0,0,0,0,1,2,3,4,5,5,5,5};
int count,uvalue,number_of_k,control_p,digree=4,max_uvalue;

#define max 30
typedef float matrix[max][4];
matrix m;


float bl(int k,int d, float u)

{

    float t=0,n=0;

     if(d==1)
  {
     if( U[k]<=u && u<=U[k+1])
  {
  return 1;
  }
  else
  return 0;
  }
   if((U[k+d-1]-U[k])==0)
 t=0;

 else
  t=(u-U[k])/(U[k+d-1]-U[k]);
  t*=bl(k,d-1,u);
 if((U[k+d]-U[k+1])==0)
  n=0;
  else
  n=(U[k+d]-u)/(U[k+d]-U[k+1]);

  n*=(bl(k+1,d-1,u));

  t+=n;
  return t;
  }

 bspline()
  {

	matrix t={-140,-30,0,1,
	-110,50,0,1,
	-90,30,0,1,
	 -50,-80,0,1,
	 -20,50,0,1,
	 50,-80,0,1,
	 80,10,0,1,
	 110,10,0,1

	};

	  int a,c,button,x,y;
	  float f=0;
	  char r;

		while(!kbhit())
	{                  getmouse(&button,&x,&y);

			  while(button==1)
		  {
			  getmouse(&button,&x,&y);
		  }


		  showmouse();

	if(getpoint(t)==-1)
	return;

	hidemouse();
	bcurve(t);
	}

    }



con( int *x, int *y,float t,matrix z)
{
 float c1=0,c2=0,c3=0,c4=0;
 int x1,y1,z1,a;

for(a=0;a<control_p;a++)
 {
   c1=bl(a,digree,t);
   c2+=c1*z[a][0];
   c3+=c1*z[a][1];
   c4+=c1*z[a][2];

   }

  *x=c2;*y=c3;z1=c4;

 change1(x,y,&z1);


 }


	bcurve(matrix t)
 {
  float n=0, s[1001];
 int a,x=0,y=0,x1=0,y1=0,b;

for(a=0;a<1000;a++)
{
if(n>=uvalue)
break;
s[a]=n;
n+=0.05;
 }
//printf(" %f   %d  %d",n,a,uvalue);

s[999]=1;

for(b=0;b<a;b++)
{
   con(&x,&y,s[b],t);
   if(b>1)
   line(x,y,x1,y1);
   x1=x;y1=y;
   //getch();



    }
}

			change1(int *x,int *y,int *z)
     {
	int i,xmax,ymax;
	xmax = getmaxx();
	ymax = getmaxy();
	*x*=2;
	*y*=2;
	*x+=*z;
	*y+=*z;

       *x=(xmax/2)+(*x);
       *y=(ymax/2)-(*y);

       }




			change2(int *x,int *y,int *z)
     {
       int i,xmax,ymax;
	xmax = getmaxx();
	ymax = getmaxy();

       *x=(*x)-(xmax/2);
       *y=(ymax/2)-(*y);

	*x/=2;
	*y/=2;

	*x-=*z;
	*y-=*z;



       }



	getpoint(matrix t)
	{
	   int x,y,button=0,a=0,one=0,z=0,i=0,tem;
	   int po[30][5];
	   getmouse(&button,&x,&y);

	   while(button!=2)
	   {

	      if (kbhit())
	      return(-1);


	     getmouse(&button,&x,&y);
	      if(button==1 && one==0)
	      {
		x-=100;
		y-=70;

	      hidemouse();
	      change2(&x,&y,&z);
	      t[a][0]=x;
	      t[a] [1]=y;
	      t[a][2]=0;
	      t[a][3]=1;

	     change1(&x,&y,&z);
	    //rectangle(x,y,x+2,y+2);
	    po[a][0]=x;
	   po[a][1]=y;


	   po[a][2]= getpixel(x,y);
	   po[a][3]= getpixel(x+1,y);
	     po[a][4]= getpixel(x+2,y);


	    putpixel(x,y,BLACK);
	    putpixel(x+1,y,BLACK);
	    putpixel(x+2,y,BLACK);


	    showmouse();

	      one=1;
	      a++;
	      }
	      if(button!=1)
	      one=0;
	    }
	    tem=1;
	    for(i=4;i<a+digree-4;i++)
	    {
	    U[i]=tem;
	    tem++;
	    }
	    for(;i<a+digree;i++)
	    U[i]=tem;
	    //printf("tem=%d",tem);
	    number_of_k=i;
	    uvalue=tem;
	    control_p=a;
	    tem=getcolor();
	    setcolor(0);
	    i=0;

	    while(i<a)
	    {

	    putpixel(po[i][0],po[i][1],po[i][2]);
	    putpixel(po[i][0]+1,po[i][1],po[i][3]);
	    putpixel(po[i][0]+2,po[i][1],po[i][4]);

		    i++;

	    }
	   setcolor(tem);

	    }

 init(char ab[])
	  {
	  int a=-60,b=640,c=-60,d=460,count=0,my1,my2,my3,my4,col;
	  screen(ab);
	  settextstyle(10,0,4);
	  setfillstyle(1,0);

	   setbkcolor(0);

	while(1)
   {
					setcolor(6);

			setfillstyle(1,0);

			bar(a,210,a+30,265);
			bar(b-5,210,b+27,265);
			bar(250,c+20,290,c+65);
			bar(315,d+5,343,d+60);

						 a+=2;
			b-=2;
			c+=2;
			d-=2;
			outtextxy(a,200,"P");
			outtextxy(b,200,"T");
		       outtextxy(250,c,"A");
			outtextxy(315,d,"N");
			 col=random(16);

			 setfillstyle(1,col);
			 if(count==0)
			 my1=col;

			 setfillstyle(1,my1);
			 floodfill(a+5,225,6);


			 if(count==5)
			  my2=col;

			 setfillstyle(1,my2);
			 floodfill(b+5,225,6);

			 if(count==10)
			  my3=col;

			 setfillstyle(1,my3);

			 floodfill(265,c+25,6);

			if(count==15)
			{
			 my4=col;
			count=-1;

			}
			 setfillstyle(1,my4);

			floodfill(320,d+25,6);
				       count++;
				       delay(30);

		       if(a>220)
			break;


	    }

	    c=-30;
		 while(1)
		 {
		       setfillstyle(1,0);
					bar(285,c+20,300,c+65);
			 c+=2;

			 outtextxy(290,c,"i");
			 setfillstyle(1,random(15));
			 floodfill(295,c+35,6);
			    delay(10);
			 if(c>200)
			 {
						 break;
			 }

		     }



	    }

