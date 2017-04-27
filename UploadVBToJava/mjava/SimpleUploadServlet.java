package coreservlet;

import javax.servlet.*;
import javax.servlet.http.*;
import java.io.*;
import java.util.*;
import java.lang.*;


public class SimpleUploadServlet extends HttpServlet {
   public void doGet(HttpServletRequest req, HttpServletResponse res)
                        throws ServletException, IOException {
        getServletContext().getRequestDispatcher("/Noget.html").forward(req, res);
   }

   public void doPost(HttpServletRequest req, HttpServletResponse res)
                        throws ServletException, IOException{

        res.setContentType("text/html/jpg/gif");

        PrintWriter out = res.getWriter();

        String contentType = req.getContentType();

        int ind = contentType.indexOf("boundary=");

        String boundary = contentType.substring(ind+9);
        int contentLength = req.getContentLength();

        String pLine = new String();

        String UploadLocation = new String();

        // a temporary holder of the file name
        String pFilename = new String();

        // the final holder of the file name
        String fFilename = new String();

        boolean uploadFlag = true;

        //the maximum size of the file can only be 50K (1024 bytes = 1K * 50)
        int MAX = 51200;

        //the reason the file failed to upload
        String reason = null;

        //System.out.println("ContentLength = " + ContentLength);


        /**
        * Here we compare to see that file going to be uploaded is
        * not greater thatn the maximum file size allowed.
        */

        if(contentLength > MAX)
        {
           reason = "File too large for upload. File not uploaded.";
           uploadFlag = false;
        }

        /**
        *  Here we compare verify that content type is multipart form data
        */

        if(contentType!=null && contentType.indexOf("multipart/form-data") == -1)
        {
           reason = "Encoding type not multipart/form-data. File not uploaded";
        }

        if(uploadFlag == true)
        {
           /** here we use the bufferedreader object to read in the Servlet's
           * InputStream
           */
           BufferedReader br = new BufferedReader(new InputStreamReader(req.getInputStream()));
           /**
           *  Here we read and hold Http headers that are not used to create the
           *  file itself
           */

           pLine = br.readLine();
           pLine = br.readLine();

           /**
           * Below we extract the filename from the Http headers
           * read os far
           */

            String filename = new String();

            out.println("Pline: "+pLine);
            filename = pLine.substring(pLine.lastIndexOf("\\")+1,pLine.lastIndexOf("\""));
            out.println("Filename: "+ filename);

           /* if the file is being uploaded from windows OS */
           if(filename.indexOf("\\") != 1)
           {
            // pFilename = pLine.substring(pLine.lastIndexOf("\\"),
            //               pLine.lastIndexOf("\""));
            //  fFilename = pFilename.substring(1);
           }else{
               fFilename = filename;
           }

           pLine = br.readLine();
           pLine = br.readLine();
           out.println("ffFilename: "+ fFilename);
           for(String line; (line=br.readLine()) != null;)
           {
              if(line.indexOf(boundary) == -1)
              {
                 out.println(UploadLocation+fFilename+"\n");
                 AppendFile(fFilename, line);
              }else{
              /**
              * if the file has been submitted in a form
              * containing form input fields
              * we want to strip these and not have it write to the
              * file
              */
                   pLine = br.readLine();
                   pLine = br.readLine();
                   pLine = br.readLine();
              }
           } //endfor

           out.println("<HTML><TITLE>Sucessfully Uploaded</TITLE>");
           out.println("<BODY>");
           out.println("<CENTER>");
           out.println("<H1>  Upload Status  </H1>");
           out.println("<BR>");
           out.println(fFilename+" was sucessfully uploaded");
           out.println("</CENTER>");
           out.println("</BODY></HTML>");
        } else {  //end upload
           out.println("<HTML><TITLE>Not Sucessfull</TITLE>");
           out.println();
           out.println("<BODY>");
           out.println("<CENTER>");
           out.println("<H1> Upload Status </H1>");
           out.println("<BR>");
           out.print(reason);
           out.println("</CENTER>");
           out.println("</BODY></HTML>");
        }
   }
   public static synchronized void AppendFile(String fname, String fileEntry)
   {
      BufferedWriter bw = null;
      try{
          bw = new BufferedWriter(new FileWriter("c:\\MB\\"+fname,true));
          bw.write(fileEntry);
          bw.newLine();
          bw.flush();
          bw.close();
      }catch(IOException ioe){
          System.out.println("IO Exception");
          System.out.println(ioe);
      }
   }
}
