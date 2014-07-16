package com.RateIt.utils;

import android.util.Log;

public class Logger {
	
	public static final boolean TRACE_ON = false;

	private static final String TAG = "RateIt";
	
	public static void e(String message, Throwable tr){
		String [] parts = parts(message);
		
		for(int i = 0; i < parts.length; i++){
			Log.e(TAG, parts[i], tr);
		}
	}
	
	public static void e(String message){
		String [] parts = parts(message);
		
		for(int i = 0; i < parts.length; i++){
			Log.e(TAG, parts[i]);
		}
	}
	
	public static void d(String message){
		String [] parts = parts(message);
		
		for(int i = 0; i < parts.length; i++){
			Log.d(TAG, parts[i]);
		}
	}

    public static void w(String message) {
        String [] parts = parts(message);

        for(int i = 0; i < parts.length; i++){
            Log.w(TAG, parts[i]);
        }
    }
	
	public static void t(String message){
		if(TRACE_ON){
			String [] parts = parts(message);
			
			for(int i = 0; i < parts.length; i++){
				Log.i(TAG, parts[i]);
			}
		}
	}
	
	private static String [] parts(String allString){
		int parts = allString.length()/500;
		if(allString.length() % 500 > 0){
			parts++;
		}
		String [] result = new String [parts];
		for(int i = 0; i < result.length; i++){
			result[i] = allString.substring(i*500, ((i+1)*500 > allString.length()) ? allString.length() : (i+1)*500);
		}
		return result;
	}

    public static void i(String s) {
        Log.i(TAG, s);
    }
}
