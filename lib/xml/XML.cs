using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.AspNetCore.Hosting;



namespace blazor.lib.xml
{
    public class EmployeeXml
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }

    }


    
    public interface IXML
    {
        List<EmployeeXml> ReadXMLAndConvertToObject(string xmlFilePath);
        void WriteXML(List<EmployeeXml> employees, string fileName);
        
    }


    public class XML : IXML
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public XML(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<EmployeeXml> ReadXMLAndConvertToObject(string xmlFilePath)
        {
            List<EmployeeXml> employees = new List<EmployeeXml>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                // Verificar se o documento tem o elemento raiz esperado
                if (xmlDoc.DocumentElement?.Name != "Employees")
                {
                    throw new InvalidOperationException("Invalid XML format. Expected 'Employees' as the root element.");
                }

                foreach (XmlNode employeeNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    if (employeeNode.Name == "Employee")
                    {
                        EmployeeXml employee = new EmployeeXml();

                        // Verificar se o elemento 'Employee' contém os elementos esperados
                        XmlNode idNode = employeeNode.SelectSingleNode("Id");
                        XmlNode nameNode = employeeNode.SelectSingleNode("Name");
                        XmlNode salaryNode = employeeNode.SelectSingleNode("Salary");

                        if (idNode != null && nameNode != null && salaryNode != null)
                        {
                            employee.Id = Convert.ToInt32(idNode.InnerText);
                            employee.Name = nameNode.InnerText;
                            employee.Salary = Convert.ToDecimal(salaryNode.InnerText);

                            // Adicione o funcionário à lista
                            employees.Add(employee);
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid XML format. 'Id', 'Name', and 'Salary' elements are required for each 'Employee'.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções durante a leitura do XML
                throw new InvalidOperationException($"Error reading XML: {ex.Message}");
            }

            return employees;
        }
        public void WriteXML(List<EmployeeXml> employees, string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement root = xmlDoc.CreateElement("Employees");
            xmlDoc.AppendChild(root);

            foreach (var employee in employees)
            {
                XmlElement employeeElement = xmlDoc.CreateElement("Employee");

                XmlElement idElement = xmlDoc.CreateElement("Id");
                idElement.InnerText = employee.Id.ToString();
                employeeElement.AppendChild(idElement);

                XmlElement nameElement = xmlDoc.CreateElement("Name");
                nameElement.InnerText = employee.Name;
                employeeElement.AppendChild(nameElement);

                XmlElement salaryElement = xmlDoc.CreateElement("Salary");
                salaryElement.InnerText = employee.Salary.ToString();
                employeeElement.AppendChild(salaryElement);

                // Add other properties as needed

                root.AppendChild(employeeElement);
            }

            // Get the path to the wwwroot/xml folder
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string xmlFolderPath = Path.Combine(wwwrootPath, "xml");

            // Check if the xml folder exists, if not, create it
            if (!Directory.Exists(xmlFolderPath))
            {
                Directory.CreateDirectory(xmlFolderPath);
            }

            // Combine the folder path with the file name
            string xmlFilePath = Path.Combine(xmlFolderPath, fileName);

            // Save the XML file
            xmlDoc.Save(xmlFilePath);
        }
    }


}
