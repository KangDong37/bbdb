using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Connect : MonoBehaviour
{
	private static MySqlConnection dbConnection;

	public int nDataCount = 0;
	public List<string> list_row1 = new List<string>();

	// Use this for initialization
	void Start()
	{
		ConnectionDB();
	}

	public void ConnectionDB()
	{
		string strConnectData = "Server=35.221.132.171;" + "port=3306;" + "Database=TEST01;" + "User ID=root;" + "Password=dino2781;";

		dbConnection = new MySqlConnection(strConnectData);

		try
		{
			dbConnection.Open();
			Debug.Log("open");

			string strQuery = "SELECT * FROM TEST01.tableTest01";

			DoQuery(strQuery);
		}
		catch (Exception err)
		{
			Debug.Log(err.ToString());
		}

		dbConnection.Close();
		Debug.Log("Done");
	}

	public void DoQuery(string srtSqlQuery)
	{
		if (srtSqlQuery.Equals(""))
			return;

		MySqlCommand dbCommand = new MySqlCommand(srtSqlQuery, dbConnection);
		MySqlDataReader reader = dbCommand.ExecuteReader();

		nDataCount = reader.FieldCount;
		while (reader.Read())
		{
			for (int i = 0; i < nDataCount; i++)
			{
				Debug.Log(reader[i] + " // ");
				list_row1.Add(reader[i].ToString());
			}
		}
		
		reader.Close();
	}

	private void OnGUI()
	{
		GUIStyle style = GUI.skin.GetStyle("label");
		style.fontSize = (int)20;

		if (list_row1[0] == null)
			return;

		GUI.Label(new Rect(20, 100, 200, 30), "row1 :: " + list_row1[0] + " // " + list_row1[1]);
		GUI.Label(new Rect(20, 150, 200, 30), "row2 :: " + list_row1[2] + " // " + list_row1[3]);
		GUI.Label(new Rect(20, 200, 200, 30), "row3 :: " + list_row1[4] + " // " + list_row1[5]);
	}
}
