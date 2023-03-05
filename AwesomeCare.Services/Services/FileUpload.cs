using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Reflection;
using System.IO;
using DocumentFormat.OpenXml;
using AwesomeCare.DataTransferObject.DTOs.Client;

namespace AwesomeCare.Services.Services
{
    public class FileUpload : IFileUpload
    {
        private IConfiguration _configuration;
        private BlobContainerClient blobContainerClient;
        public FileUpload(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFile(string folder, bool isPublic, string filename, Stream fileStream, string contentType)
        {
            string storageConnectionString = _configuration["BlobConnectionString"];
            blobContainerClient = new BlobContainerClient(storageConnectionString, folder);
            await blobContainerClient.CreateIfNotExistsAsync();

            // Set the permissions so the blobs are public. 
            if (isPublic)
                await blobContainerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            BlobClient blob = blobContainerClient.GetBlobClient(filename);
            fileStream.Position = 0;
            await blob.UploadAsync(fileStream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = contentType } });
            return blob.Uri.AbsoluteUri;
        }

        public async Task<Tuple<Stream, string>> DownloadFile(string filename)
        {
            var blobUriBuilder = new Azure.Storage.Blobs.BlobUriBuilder(new Uri(filename));
            var containerName = blobUriBuilder.BlobContainerName;
            var blobname = blobUriBuilder.BlobName;
            string storageConnectionString = _configuration["BlobConnectionString"];
            blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);

            BlobClient blob = blobContainerClient.GetBlobClient(blobname);
            var stream = new MemoryStream();

            var properties = await blob.GetPropertiesAsync();
            var contentType = properties.Value.ContentType;
            await blob.DownloadToAsync(stream);
            return new Tuple<Stream, string>(stream, contentType);


        }
        public MemoryStream DownloadClientFile(Object entity)
        {
            var stream = new MemoryStream();
            
            #region Word Document
            using (WordprocessingDocument doc =
                WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mdp = doc.MainDocumentPart;
                mdp = doc.AddMainDocumentPart();
                Document document = new Document(new Body());
                string Status = "";
                
                #region Head
                Table head = new Table();
                TableProperties headProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dotted), Size = 12, Color = "red" },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Dotted), Size = 12, Color = "red" },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) }
                    )
                );
                head.AppendChild<TableProperties>(headProp);
                List<TableRow> headrow = new List<TableRow>();
                List<TableCell> headcell = new List<TableCell>();
                
                int cel = 0;
                int row = 0;
                Type type = entity.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var vlue = property.GetValue(entity, null);
                    if (property.Name == "ClientName" || property.Name == "IdNumber" || property.Name == "DOB")
                    {
                        Paragraph headpara = new Paragraph();
                        Run headrun = new Run();
                        RunProperties headrunProp = new RunProperties();

                        RunFonts headrunFont = new RunFonts { Ascii = "Century Gothic" };
                        FontSize headfont = new FontSize { Val = "20" };
                        Color headcolor = new Color { Val = "light grey" };
                        headrunProp.AppendChild(headrunFont);
                        headrunProp.AppendChild(headcolor);
                        headrunProp.AppendChild(headfont);
                        headpara.AppendChild(headrun);
                        headpara.AppendChild(headrunProp);
                        headrun.AppendChild(new Text(property.Name + ": \n" + vlue.ToString()));
                        if(cel == 0)
                            headrow.Add(new TableRow());
                        headcell.Add(new TableCell());
                        headcell[cel].AppendChild(headpara);
                        headrow[row].AppendChild(headcell[cel]);
                        cel++;
                    }
                }
                #endregion

                #region Table
                Table _table = new Table();
                TableProperties _tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) }
                    )
                );
                _table.AppendChild<TableProperties>(_tblProp);
                List<TableRow> trow = new List<TableRow>();
                List<TableCell> tcells = new List<TableCell>();
                cel = 0;
                row = 0;

                Table xtable = new Table();
                TableProperties xtblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Nil) }
                    )
                );
                xtable.AppendChild<TableProperties>(xtblProp);
                List<TableRow> xtrow = new List<TableRow>();
                List<TableCell> xtcells = new List<TableCell>();
                int xcel = 0;
                int xrow = 0;
                foreach (PropertyInfo property in properties)
                {
                    var vlue = property.GetValue(entity, null);
                    if (property.Name == "Attach" || property.Name == "Staffs" || property.Name == "ListItems" || property.Name == "StaffNameList" || property.Name == "OfficerToActList" || property.Name == "PhysicianList" || property.Name == "OFFICERTOACTList" || property.Name == "CallerList" || property.Name == "BestSupportList" || property.Name == "PoorSupportList" || property.Name == "StatusList" || property.Name == "InterestedInProgramList" || property.Name == "RateServiceRecievingList" || property.Name == "ClientList"
                        || property.Name == "ClientId" || property.Name.Contains("Id") || property.Name.Contains("StaffList")) continue;

                    if (vlue != null)
                    {
                        if (!vlue.ToString().Equals("0"))
                        {
                            if (property.Name == "StatusName")
                                Status = "\t \t"+vlue.ToString();
                            if (vlue.ToString().Length <= 35)
                            {
                                var paragraph = new Paragraph();
                                var tablerun = paragraph.AppendChild(new Run());

                                RunProperties runProp = tablerun.AppendChild(new RunProperties());
                                RunFonts _font = new RunFonts { Ascii = "Century Gothic" };
                                FontSize _fontsize = new FontSize { Val = new StringValue("20") };
                            
                                runProp.Append(_font);
                                runProp.Append(_fontsize);
                                runProp.AppendChild(new Justification() { Val = JustificationValues.Left });
                                tablerun.AppendChild(new Text(property.Name + ": \n" + vlue + " \n"));

                                if (cel % 3 == 0)
                                    trow.Add(new TableRow());
                                tcells.Add(new TableCell());
                                tcells[cel].AppendChild(paragraph);
                                trow[row].AppendChild(tcells[cel]);
                                cel++;
                                if (cel % 3 == 0)
                                    row++;
                            }
                            if (vlue.ToString().Length > 35)
                            {
                                var paragraph = new Paragraph();
                                var tablerun = paragraph.AppendChild(new Run());

                                RunProperties runProp = tablerun.AppendChild(new RunProperties());
                                RunFonts _font = new RunFonts { Ascii = "Century Gothic" };
                                FontSize _fontsize = new FontSize { Val = new StringValue("20") };

                                runProp.Append(_font);
                                runProp.Append(_fontsize);
                                runProp.AppendChild(new Justification() { Val = JustificationValues.Left });
                                tablerun.AppendChild(new Text(property.Name + ": \n" + vlue + " \n"));

                                xtrow.Add(new TableRow());
                                xtcells.Add(new TableCell());
                                xtcells[xcel].AppendChild(paragraph);
                                xtrow[xrow].AppendChild(xtcells[xcel]);
                                xcel++;
                                xrow++;
                            }
                        }
                    }
                }
                #endregion

                #region Company
                Table table = new Table();
                Paragraph para = new Paragraph();
                Run run = para.AppendChild(new Run());
                RunProperties runProperties = new RunProperties();
                RunFonts tblFont = new RunFonts { Ascii = "Century Gothic" };
                FontSize font = new FontSize { Val = "36" };
                Color color = new Color { Val = "red" };
                runProperties.AppendChild(tblFont);
                runProperties.AppendChild(color);
                runProperties.AppendChild(font);
                runProperties.AppendChild(new Justification() { Val = JustificationValues.Left });
                run.AddChild(runProperties);

                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.None) }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);
                run.AppendChild(new Text("AWESOME HEALTHCARE"));
                List<TableRow> tblrow = new List<TableRow>();
                List<TableCell> tblcell = new List<TableCell>();
                tblrow.Add(new TableRow());
                tblcell.Add(new TableCell());
                tblcell[0].AppendChild(para);
                tblrow[0].AppendChild(tblcell[0]);

                Paragraph spara = new Paragraph();
                Run srun = spara.AppendChild(new Run());
                RunProperties srunProperties = new RunProperties();
                RunFonts stblFont = new RunFonts { Ascii = "Century Gothic" };
                FontSize sfont = new FontSize { Val = "24" };
                Color scolor = new Color { Val = "red" };
                srunProperties.AppendChild(stblFont);
                srunProperties.AppendChild(scolor);
                srunProperties.AppendChild(sfont);
                srunProperties.AppendChild(new Justification() { Val = JustificationValues.Right });
                srun.AppendChild(srunProperties);
                srun.AppendChild(new Text(Status));

                tblcell.Add(new TableCell());
                tblcell[1].AppendChild(spara);
                tblrow[0].AppendChild(tblcell[1]);

                tblrow.Add(new TableRow());
                tblcell.Add(new TableCell());
                tblcell[2].AppendChild(new Paragraph(new Run(new Text("[Street Address, City, ST ZIP Code]"))));
                tblrow[1].AppendChild(tblcell[2]);
                #endregion

                #region Inject
                foreach (TableRow item in tblrow)
                {
                    table.AppendChild(item);
                }
                foreach (TableRow item in headrow)
                {
                    head.AppendChild(item);
                }
                foreach (TableRow item in trow)
                {
                    _table.AppendChild(item);
                }
                foreach (TableRow item in xtrow)
                {
                    xtable.AppendChild(item);
                }
                document.AppendChild(table);
                document.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.TextWrapping })));
                document.AppendChild(head);
                document.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.TextWrapping })));
                document.AppendChild(_table);
                document.AppendChild(new Paragraph(new Run(new Break() { Type = BreakValues.TextWrapping })));
                document.AppendChild(xtable);
                document.Save(mdp);

                mdp.Document.Save();
                #endregion

            }
            return stream;
            #endregion
        }
    }
}
