package com.RateIt.network;

import android.content.Context;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import com.RateIt.utils.Logger;
import org.apache.http.HttpResponse;
import org.apache.http.HttpStatus;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.protocol.ClientContext;
import org.apache.http.impl.client.BasicCookieStore;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.protocol.BasicHttpContext;

import java.io.IOException;
import java.io.InputStream;

abstract public class ApiFunction {
    public static final String hostName = "192.168.0.95";
	public static final String SERVER = "http://"+hostName+"/RateIt.Web/RateItFacade.svc/";
	public static final String BASE_URL = SERVER + "api/";//"api/2/";
	
	private static final String USERNAME = "muser";
	private static final String PASSWORD = "mwdn_demo_2013";
    private static final String STUDY_NUMB = "1.2.3.4";
    //private static final String STUDY_NUMB = "1.2.840.113704.1.111.3372.1270643614.60"; // Demo - James White - j2k
    //private static final String STUDY_NUMB = "1.2.840.113680.4.103.82811.20110318110545"; // Cine 1 - jpeg
    //private static final String STUDY_NUMB = "1.2.410.200001.1.6.1.20091218215324125";    // Cine 2 - jpeg - cines are shorter
    //private static final String STUDY_NUMB = "1.2.392.200036.9123.100.12.11.17030.2007050814183815";// - Demo  - j2k - smallest
    //private static final String STUDY_NUMB = "1.2.840.113619.2.55.3.1678428446.6943.1186748965.401"; // Malinovski Roman - Noga  - j2k
    //private static final String STUDY_NUMB = "1.2.840.113704.1.111.3236.1206632331.1";// - Auld Gertrude P - 4 seris, 4-1-744-116

	public static final String PATIENT_INFO = BASE_URL + "PatientInfo/";
	private static final String STUDY_INFO = BASE_URL + "StudyInfo/";
	
	public static final String SERIES_INFO = BASE_URL + "SeriesInfo/";
	public static final String INSTANCE_INFO = BASE_URL + "InstanceInfo/";
	public static final String INSTANCE_DATA = BASE_URL + "InstanceData/";
    public static final String ANNOTATIONS_URL = BASE_URL + "AnnotationData/";

    public static final String USER_LOGIN = SERVER + "UserLoginA/";
	
	public static final String STUDY_KEY = "StudyInfo";
	
	static BasicCookieStore cookieStore = new BasicCookieStore();
	
	protected Context context;
	
	protected String url;
	
	public ApiFunction(Context context, String url){
		this.context = context;
		this.url = url;
	}

	public static final String getStudyInfo(SharedPreferences shPref){
		if(shPref == null){
			return STUDY_INFO + STUDY_NUMB;
		}
		String numb = shPref.getString(STUDY_KEY, STUDY_NUMB);
		return STUDY_INFO + numb;
	}
	
	public boolean call(){
		if(context == null){
			return false;
		}
		ConnectivityManager conMng = (ConnectivityManager)context.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo netInfo = conMng.getActiveNetworkInfo();
		if(netInfo == null || !netInfo.isConnected()){
			return false;
		}
		
		return get(url, true);
	}
	
	private boolean get(String url, boolean retryOnError){
		long startTime = System.currentTimeMillis();
		boolean error = false;
		try{
			if(Logger.TRACE_ON){
				Logger.d(url);
			}
			HttpGet httpGet = new HttpGet(url);
			
			HttpClient httpClient = new DefaultHttpClient();

			HttpResponse response = httpClient.execute(httpGet);

			int status = response.getStatusLine().getStatusCode();
			if(status != HttpStatus.SC_OK){
				Logger.e(response.getStatusLine().getReasonPhrase());
				error = true;
			}
			if(!error){
				InputStream content = response.getEntity().getContent();
				try{
					error = !parse(content);
				}catch(Exception e){
					error = true;
					Logger.e("apiFunc.parse", e);
				}
				content.close();
			}
		}catch(Exception e){
			error = true;
			Logger.e("apiFunc.post", e);
		}
		
		if(error && retryOnError && System.currentTimeMillis() - startTime < 2000){
			Logger.d("Call rejected fast");
			try{
				Thread.sleep(500);
			}catch(Exception ignored){}
			
			return get(url, false);
		}
		return !error;
	}
	
	abstract protected boolean parse(InputStream is) throws IOException;
}
