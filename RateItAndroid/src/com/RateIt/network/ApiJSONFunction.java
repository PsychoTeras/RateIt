package com.RateIt.network;

import android.content.Context;
import android.text.TextUtils;
import com.RateIt.utils.Logger;
import com.RateIt.network.ApiFunction;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;

public class ApiJSONFunction extends ApiFunction {
	
	private JSONObject response;
	
	public ApiJSONFunction(Context context, String url){
		super(context, url);
	}
	
	public boolean call(){
		boolean result = super.call();
		if(!result){
			return false;
		}
		if(response== null){
			return false;
		}
		return true;
	}
	
	public JSONObject getResponse(){
		return response;
	}

	@Override
	protected boolean parse(InputStream is) throws IOException {
		ByteArrayOutputStream bos = new ByteArrayOutputStream();
		byte [] buff = new byte[1024];

        int length = is.read(buff);

		while(length >= 0){
			bos.write(buff, 0, length);
			length = is.read(buff);
		}

		try{
            String body = bos.toString();
			JSONObject obj = new JSONObject(body);
			if(Logger.TRACE_ON){
				Logger.t(bos.toString());
			}
			response = obj;
			return obj != null;
		}catch(Exception e){
			Logger.e("apiJsonFunc.parse.parseJson", e);
		}
		return false;
	}

    public static int getInt(JSONObject obj, String key) throws JSONException{
        return getInt(obj, key, 0);
    }

	public static int getInt(JSONObject obj, String key, int defaultVal) throws JSONException{
		try{
            if (!obj.has(key))
                return defaultVal;

			String string = obj.getString(key);
			if(!TextUtils.isEmpty(string)){
				return Integer.parseInt(string);
			}
		}catch(Exception e){
			Logger.e("ApiJsonFunc:getInt:", e);
		}
		return defaultVal;
	}
	
	public static long getLong(JSONObject obj, String key) throws JSONException{
		try{
			String string = obj.getString(key);
			if(!TextUtils.isEmpty(string)){
				return Long.parseLong(string);
			}
		}catch(Exception e){
			Logger.e("ApiJsonFunc:getLong:", e);
		}
		return 0;
	}


    public static float getFloat(JSONObject obj, String key) throws JSONException{
        return getFloat(obj, key, 0);
    }

	public static float getFloat(JSONObject obj, String key, float defaultVal) throws JSONException{
		try{
            if (!obj.has(key))
                return defaultVal;

			String string = obj.getString(key);
			if(!TextUtils.isEmpty(string)){
				return Float.parseFloat(string);
			}
		}catch(Exception e){
			Logger.e("ApiJsonFunc:getFloat:", e);
		}
		return defaultVal;
	}
	
	public static double getDouble(JSONObject obj, String key) throws JSONException{
		try{
			String string = obj.getString(key);
			if(!TextUtils.isEmpty(string)){
				return Double.parseDouble(string);
			}
		}catch(Exception e){
			Logger.e("ApiJsonFunc:getDouble:", e);
		}
		return 0;
	}
	
	public static boolean getBoolean(JSONObject obj, String key) throws JSONException{
		try{
			String string = obj.getString(key);
			if(!TextUtils.isEmpty(string)){
				return Boolean.parseBoolean(string);
			}
		}catch(Exception e){
			Logger.e("ApiJsonFunc:getBoolean:", e);
		}
		return false;
	}
}
